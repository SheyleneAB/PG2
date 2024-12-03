using Microsoft.AspNetCore.Mvc;

namespace StripsRest.Controllers
{
    public class AuteurController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
