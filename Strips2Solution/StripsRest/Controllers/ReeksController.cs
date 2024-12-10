using Microsoft.AspNetCore.Mvc;

namespace StripsRest.Controllers
{
    public class ReeksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
