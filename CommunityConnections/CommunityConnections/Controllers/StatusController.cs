using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunityConnections.Models;

namespace CommunityConnections.Controllers
{
    public class StatusController : Controller
    {
        private readonly localnewsContext _context;

        public StatusController(localnewsContext context)
        {
            _context = context;
        }

        // GET: Status
        public async Task<IActionResult> Index()
        {
              return _context.Statuses != null ? 
                          View(await _context.Statuses.ToListAsync()) :
                          Problem("Entity set 'localnewsContext.Statuses'  is null.");
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Statuses == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses
                .FirstOrDefaultAsync(m => m.StatusName == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Status/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusName")] Status status)
        {
            if (ModelState.IsValid)
            {
                _context.Add(status);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Statuses == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StatusName")] Status status)
        {
            if (id != status.StatusName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(status);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.StatusName))
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
            return View(status);
        }

        // GET: Status/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Statuses == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses
                .FirstOrDefaultAsync(m => m.StatusName == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Statuses == null)
            {
                return Problem("Entity set 'localnewsContext.Statuses'  is null.");
            }
            var status = await _context.Statuses.FindAsync(id);
            if (status != null)
            {
                _context.Statuses.Remove(status);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(string id)
        {
          return (_context.Statuses?.Any(e => e.StatusName == id)).GetValueOrDefault();
        }
    }
}
