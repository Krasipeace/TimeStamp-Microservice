namespace TimestampMicroservice.App.Services
{
    using System.Text.Json;

    using TimestampMicroservice.App.Contracts;
    using TimestampMicroservice.App.Models;

    using static TimestampMicroservice.Common.AppConstants;

    public class TimestampService : ITimestampService
    {
        private readonly HttpClient httpClient;

        public TimestampService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<TimeStampViewModel> GetTimestampAsync()
        {
            var response = await this.httpClient.GetAsync(DockerApiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(InvalidResponseExceptionMessage);
            }

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TimeStampViewModel>(content)!;
        }

        public Task<HumanDateTimeViewModel> GetHumanDateTimeAsync()
        {
            return Task.FromResult(new HumanDateTimeViewModel());
        }
    }
}
