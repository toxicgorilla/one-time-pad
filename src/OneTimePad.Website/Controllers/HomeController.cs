using Microsoft.AspNetCore.Mvc;

namespace OneTimePad.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
