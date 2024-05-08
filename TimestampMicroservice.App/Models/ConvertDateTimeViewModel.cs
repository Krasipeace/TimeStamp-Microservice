namespace TimestampMicroservice.App.Models;

using System.ComponentModel.DataAnnotations;

public class ConvertDateTimeViewModel
{
    [Range(0, long.MaxValue)]
    public long EpochTime { get; set; }

    [Range(0, long.MaxValue)]
    public long EpochTimeLocal { get; set; }

    public string DateAndTime { get; set; } = string.Empty;
}
