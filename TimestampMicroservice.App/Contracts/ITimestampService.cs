namespace TimestampMicroservice.App.Contracts
{
    using TimestampMicroservice.App.Models;

    public interface ITimestampService
    {
        Task<TimeStampViewModel> GetTimestampAsync();

        Task<HumanDateTimeViewModel> GetHumanDateTimeAsync();
    }
}
