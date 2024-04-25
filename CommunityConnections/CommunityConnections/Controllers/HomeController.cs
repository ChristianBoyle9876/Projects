using CommunityConnections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace EnterpriseProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly localnewsContext _context;

        public HomeController(ILogger<HomeController> logger, localnewsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Alerts()
        {

            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetGlobalUsername(string selectedUsername, string password)
        {
            var isValidUser = ValidateLogin(selectedUsername, password);

            if (isValidUser)
            {
                ChooseUser.SelectedUsername = selectedUsername;
                return RedirectToAction("Index", "Alerts");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Username and/or Password";
                return View("Index");
            }
        }

        [HttpGet]
        public IActionResult Auth()
        {
            string selectedUsername = ChooseUser.SelectedUsername;

            var user = _context.Users.FirstOrDefault(u => u.UserName == selectedUsername);

            if (user != null)
            {
                string authQuestion = user.AuthQuestion;

                ViewBag.AuthQuestion = authQuestion;
                ViewBag.AuthorizationQuestions = GetUserAuthorizationQuestions();

                return View("Auth");
            }
            else
            {
                ViewBag.ErrorMessage = "User not found";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Auth(string authAnswer)
        {
            string selectedUsername = ChooseUser.SelectedUsername;

            var user = _context.Users.FirstOrDefault(u => u.UserName == selectedUsername);

            if (user != null)
            {
                var isValidUser = ValidateAuth(user.AuthQuestion, authAnswer);

                if (isValidUser)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Answer";
                    ViewBag.AuthQuestion = user.AuthQuestion;
                    ViewBag.AuthorizationQuestions = GetUserAuthorizationQuestions();
                    return View("Auth");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "User not found";
                return RedirectToAction("Index");
            }
        }

        private bool ValidateLogin(string selectedUsername, string password)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.UserName == selectedUsername &&
                u.Password == password);

            return user != null;
        }

        private bool ValidateAuth(string authQuestion, string authAnswer)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.AuthQuestion == authQuestion &&
                u.AuthAnswer == authAnswer);

            return user != null;
        }

        private IEnumerable<string> GetUserAuthorizationQuestions()
        {
            return _context.Users.Select(u => u.AuthQuestion).Distinct();
        }
    }
}