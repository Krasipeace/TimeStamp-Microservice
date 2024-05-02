namespace TimestampMicroservice.App.Models;

using System.ComponentModel.DataAnnotations;
using System.Configuration;

using static TimestampMicroservice.Common.AppConstants;

public class HumanDateTimeViewModel
{
    [StringLength(StringMaxLength, MinimumLength = StringMinLength)]
    public string Utc { get; set; } = string.Empty;

    [StringLength(StringMaxLength, MinimumLength = StringMinLength)]
    public string Local { get; set; } = string.Empty;

    [RegexStringValidator(TimestampInputRegexValidation)]
    public string? Timestamp { get; set; }
}
