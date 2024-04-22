namespace TimestampMicroservice.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AppController : Controller
    {
        public IActionResult GetCurrent()
        {
            return View("Timestamp");
        }

        public IActionResult GetDateTime()
        {
            return View("DateTime");
        }
    }
}
