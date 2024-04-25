using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunityConnections.Models;
using Newtonsoft.Json.Linq;

namespace EnterpriseProject.Controllers
{
    public class AlertsController : Controller
    {
        private readonly localnewsContext _context;

        public AlertsController(localnewsContext context)
        {
            _context = context;
        }

        // GET: Alerts
        public async Task<IActionResult> Index()
        {
            var localnewsContext = _context.Alerts.Include(a => a.StatusNavigation).Include(a => a.UserNameNavigation);
            return View(await localnewsContext.ToListAsync());
        }

        // GET: Alerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alerts == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts
                .Include(a => a.StatusNavigation)
                .Include(a => a.UserNameNavigation)
                .FirstOrDefaultAsync(m => m.AlertId == id);
            if (alert == null)
            {
                return NotFound();
            }

            return View(alert);
        }

        // GET: Alerts/Create
        public IActionResult Create()
        {
            ViewData["Status"] = new SelectList(_context.Statuses, "StatusName", "StatusName");
            ViewData["UserName"] = new SelectList(_context.Users, "UserName", "UserName");
            return View();
        }

        // POST: Alerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlertTitle,AlertType,Location,Zipcode,TimePosted,AlertDescription,Status,UserName")] Alert alert)
        {
            if (ModelState.IsValid)
            {

                // Set the TimePosted property to the current date and time
                alert.TimePosted = DateTime.Now;

                // Set the UserName before saving the alert
                alert.UserName = ChooseUser.SelectedUsername;

                // Assuming you have a DbContext named _context
                _context.Add(alert);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // If the model state is not valid, return to the create view
            return View(alert);
        }

        // GET: Alerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alerts == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts.FindAsync(id);
            if (alert == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(_context.Statuses, "StatusName", "StatusName", alert.Status);
            ViewData["UserName"] = new SelectList(_context.Users, "UserName", "UserName", alert.UserName);
            return View(alert);
        }

        // POST: Alerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlertId,TimePosted,AlertType,AlertTitle,AlertDescription,Zipcode,Location,Status,UserName")] Alert alert)
        {
            if (id != alert.AlertId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlertExists(alert.AlertId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Status"] = new SelectList(_context.Statuses, "StatusName", "StatusName", alert.Status);
            ViewData["UserName"] = new SelectList(_context.Users, "UserName", "UserName", alert.UserName);
            return View(alert);
        }

        // GET: Alerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alerts == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts
                .Include(a => a.StatusNavigation)
                .Include(a => a.UserNameNavigation)
                .FirstOrDefaultAsync(m => m.AlertId == id);
            if (alert == null)
            {
                return NotFound();
            }

            return View(alert);
        }

        // POST: Alerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alerts == null)
            {
                return Problem("Entity set 'localnewsContext.Alerts'  is null.");
            }
            var alert = await _context.Alerts.FindAsync(id);
            if (alert != null)
            {
                _context.Alerts.Remove(alert);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlertExists(int id)
        {
            return (_context.Alerts?.Any(e => e.AlertId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> SearchByZipAndType(string alertType, string zipcode)
        {
            string apiKey = "6b58250683ee45f4832154127241903";
            string apiUrl = $"https://api.weatherapi.com/v1/current.json?key={apiKey}&q={zipcode}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject weatherData = JObject.Parse(json);

                    string iconUrl = weatherData["current"]["condition"]["icon"].ToString();
                    string location = weatherData["location"]["name"].ToString();
                    double tempFahrenheit = (double)weatherData["current"]["temp_f"];

                    ViewBag.WeatherIcon = iconUrl;
                    ViewBag.Location = location;
                    ViewBag.TemperatureFahrenheit = tempFahrenheit;
                }
                else
                {
                    ViewBag.WeatherError = "Failed to fetch weather data";
                }
            }

            IQueryable<Alert> alerts = _context.Alerts;

            if (!string.IsNullOrEmpty(alertType) && !string.IsNullOrEmpty(zipcode))
            {
                alerts = alerts.Where(a => a.AlertType == alertType && a.Zipcode == zipcode);
            }
            else if (!string.IsNullOrEmpty(alertType))
            {
                alerts = alerts.Where(a => a.AlertType == alertType);
            }
            else if (!string.IsNullOrEmpty(zipcode))
            {
                alerts = alerts.Where(a => a.Zipcode == zipcode);
            }

            ViewBag.CurrentZipcode = zipcode;
            ViewBag.SelectedAlertType = alertType;

            return View("Index", alerts.ToList());
        }

        // GET: Alerts/SearchByZip
        /*public async Task<IActionResult> SearchByZip(string zipcode)
        {
            ViewBag.CurrentZipcode = zipcode;
            // Make API call to weatherapi.com
            string apiKey = "6b58250683ee45f4832154127241903"; 
            string apiUrl = $"https://api.weatherapi.com/v1/current.json?key={apiKey}&q={zipcode}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject weatherData = JObject.Parse(json);

                    // Extract relevant weather information
                    string iconUrl = weatherData["current"]["condition"]["icon"].ToString();
                    string location = weatherData["location"]["name"].ToString();
                    double tempFahrenheit = (double)weatherData["current"]["temp_f"];

                    ViewBag.WeatherIcon = iconUrl;
                    ViewBag.Location = location;
                    ViewBag.TemperatureFahrenheit = tempFahrenheit;
                }
                else
                {
                    // Handle API call failure
                    ViewBag.WeatherError = "Failed to fetch weather data";
                }
            }

            // Retrieve alerts for the provided ZIP code
            var alerts = _context.Alerts.Where(a => a.Zipcode == zipcode).ToList();
            return View("Index", alerts);

        }

        // GET: Alerts/FilterByType
        // GET: Alerts/FilterByType
        public IActionResult FilterByTypeAndZip(string alertType, string zipcode)
        {
            IQueryable<Alert> alerts = _context.Alerts;

            // Filter by alert type and zipcode simultaneously
            if (!string.IsNullOrEmpty(alertType) && !string.IsNullOrEmpty(zipcode))
            {
                alerts = alerts.Where(a => a.AlertType == alertType && a.Zipcode == zipcode);
            }
            // Filter by alert type only
            else if (!string.IsNullOrEmpty(alertType))
            {
                alerts = alerts.Where(a => a.AlertType == alertType);
            }
            // Filter by zipcode only
            else if (!string.IsNullOrEmpty(zipcode))
            {
                alerts = alerts.Where(a => a.Zipcode == zipcode);
            }

            ViewBag.CurrentZipcode = zipcode; // Pass the zipcode to the view
            ViewBag.SelectedAlertType = alertType; // Pass the selected alert type to the view

            return View("Index", alerts.ToList());
        }*/
    }
}