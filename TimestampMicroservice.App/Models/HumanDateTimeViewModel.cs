namespace TimestampMicroservice.App.Models
{
    using System.ComponentModel.DataAnnotations;

    using static TimestampMicroservice.Common.AppConstants;

    public class HumanDateTimeViewModel
    {
        [StringLength(InputMaxLength, MinimumLength = InputMinLength)]
        public string Utc { get; set; } = string.Empty;

        [StringLength(InputMaxLength, MinimumLength = InputMinLength)]
        public string Local { get; set; } = string.Empty;

        [Range(0, long.MaxValue)]
        public string? Timestamp { get; set; } 
    }
}
