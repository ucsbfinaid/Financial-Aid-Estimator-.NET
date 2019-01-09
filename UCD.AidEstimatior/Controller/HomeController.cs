using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        /// <summary>
        /// Aid estimator homepage
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["Title"] = "";
            ViewData["IntroText"] = "PLACEHOLDER Intro Text";

            return View();
        }

        /// <summary>
        /// Error page
        /// </summary>
        /// <returns></returns>
        [Route("/[action]")]
        [IgnoreAntiforgeryToken]
        public IActionResult Error()
        {
            return View();
        }

    }
}
