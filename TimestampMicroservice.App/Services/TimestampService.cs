namespace TimestampMicroservice.App.Services;

using Newtonsoft.Json;

using TimestampMicroservice.App.Models;
using TimestampMicroservice.App.Services.Contracts;

public class TimestampService : ITimestampService
{
    private readonly HttpClient httpClient;
    /// <summary>
    /// You can set up BASE_URL .env variable for docker or other start-up options, If not set up, it falls to localhost Uri.
    /// </summary>
    Uri baseUrl = new(Environment.GetEnvironmentVariable("BASE_URL") ?? "https://localhost:44340/api/Timestamp/");

    public TimestampService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        this.httpClient.BaseAddress = baseUrl;
    }

    public async Task<TimeStampViewModel> GetCurrentTimeAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
        string jsonResponse = await response.Content.ReadAsStringAsync();

        var viewModel = JsonConvert.DeserializeObject<TimeStampViewModel>(jsonResponse);

        TimeStampViewModel model = new()
        {
            Unix = viewModel!.Unix,
            Utc = viewModel.Utc,
            Local = viewModel.Local
        };

        return model;
    }

    public async Task<HumanDateTimeViewModel> GetDateTimeAsync(string timestamp)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}{timestamp}");
        string jsonResponse = await response.Content.ReadAsStringAsync();

        var viewModel = JsonConvert.DeserializeObject<TimeStampViewModel>(jsonResponse);

        HumanDateTimeViewModel model = new()
        {
            Utc = viewModel!.Utc,
            Local = viewModel.Local
        };

        return model;
    }

    public async Task<ConvertDateTimeViewModel> ConvertDateTimeAsync(string dateTime)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}{dateTime}");
        string jsonResponse = await response.Content.ReadAsStringAsync();

        var viewModel = JsonConvert.DeserializeObject<ConvertDateTimeViewModel>(jsonResponse);

        ConvertDateTimeViewModel model = new()
        {
            EpochTime = viewModel!.EpochTime,
            EpochTimeLocal = viewModel.EpochTimeLocal,
        };

        return model;
    }
}
