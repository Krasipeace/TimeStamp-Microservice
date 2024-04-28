namespace TimestampMicroservice.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    using TimestampMicroservice.App.Models;
    using TimestampMicroservice.App.Services.Contracts;

    public class TimestampController : Controller
    {
        private readonly ITimestampService timeService;

        public TimestampController(ITimestampService timeService)
        {
            this.timeService = timeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrent()
        {
            var model = await timeService.GetCurrentTimeAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult GetDateTime()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetDateTime(string timestamp)
        {
            var result = await timeService.GetDateTimeAsync(timestamp);

            return View(result);
        }
    }
}
