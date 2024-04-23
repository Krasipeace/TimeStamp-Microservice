namespace TimestampMicroservice.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    using TimestampMicroservice.App.Models;

    public class TimestampController : Controller
    {
        private readonly HttpClient httpClient;
        readonly Uri baseUrl = new("https://localhost:44340/api/Timestamp/");

        public TimestampController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = baseUrl;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrent()
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<TimeStampViewModel>(jsonResponse);

            TimeStampViewModel model = new();
            model.Unix = viewModel!.Unix;
            model.Utc = viewModel.Utc;
            model.Local = viewModel.Local;

            return View(model);
        }

        public IActionResult GetDateTime()
        {
            return View("DateTime");
        }
    }
}
