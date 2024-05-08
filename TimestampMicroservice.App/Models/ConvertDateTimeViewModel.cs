namespace TimestampMicroservice.App.Models;

public class ConvertDateTimeViewModel
{
    public string? EpochTime { get; set; }

    public string? EpochTimeLocal { get; set; }

    public string DateAndTime { get; set; } = string.Empty;
}
