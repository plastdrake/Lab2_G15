using Microsoft.AspNetCore.Mvc;

namespace ConcertBookingAPI.Controllers
{
    public class PerformancesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
