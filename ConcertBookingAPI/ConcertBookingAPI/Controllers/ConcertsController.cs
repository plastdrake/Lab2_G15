using Microsoft.AspNetCore.Mvc;

namespace ConcertBookingAPI.Controllers
{
    public class ConcertsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
