namespace TimestampMicroservice.App.Services.Contracts;

using System.Threading.Tasks;

using TimestampMicroservice.App.Models;

public interface ITimestampService
{
    Task<TimeStampViewModel> GetCurrentTimeAsync();
    Task<HumanDateTimeViewModel> GetDateTimeAsync(string timestamp);
    Task<ConvertDateTimeViewModel> ConvertDateTimeAsync(DateTime dateTime);
}
