namespace TimestampMicroservice.Common;

public static class AppConstants
{
    public const int ReleaseYear = 2024;

    public const int StringMaxLength = 60;
    public const int StringMinLength = 8;

    public const string InvalidResponseExceptionMessage = "Cannot retrieve timestamp from the server.";

    public const string TimestampInputRegexValidation = "@[1-9]\\d{0,12}";

    public const string GetCurrentViewTitle = "Get Unix Timestamp";
    public const string GetDateTimeViewTitle = "Paste Timestamp and get Date and Time";
}
