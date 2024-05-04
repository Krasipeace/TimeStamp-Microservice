namespace TimestampMicroservice.App.Services;

using Humanizer;

using Newtonsoft.Json;

using NuGet.Packaging.Signing;

using System.Text;

using TimestampMicroservice.App.Models;
using TimestampMicroservice.App.Services.Contracts;

public class TimestampService : ITimestampService
{
    private readonly HttpClient httpClient;
    readonly Uri baseUrl = new(Environment.GetEnvironmentVariable("BASE_URL") ?? "https://localhost:44340/api/Timestamp/");

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

    public async Task<ConvertDateTimeViewModel> ConvertDateTimeAsync(DateTime dateTime)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}convert/{dateTime}");
        string jsonResponse = await response.Content.ReadAsStringAsync();

        var viewModel = JsonConvert.DeserializeObject<ConvertDateTimeViewModel>(jsonResponse);

        return viewModel!;
    }
}
