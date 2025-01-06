using Microsoft.AspNetCore.Mvc;

namespace ConcertBookingAPI.Controllers
{
    public class BookingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
