namespace TimestampMicroservice.App.Models;

using System.ComponentModel.DataAnnotations;

public class ConvertDateTimeViewModel
{
    [Range(0, long.MaxValue)]
    public long EpochTime { get; set; }

    public DateTime DateAndTime { get; set; } = DateTime.Now;
}
