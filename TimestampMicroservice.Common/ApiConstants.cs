namespace TimestampMicroservice.Common;

public static class ApiConstants
{
    public const string DateTimeStringFormat = "dd-MM-yyyy HH:mm:ss";

    public const string InvalidTimestampInputErrorMessage = "Invalid Timestamp";

    public const string TimestampOutOfRangeExceptionMessage = "Timestamp is out of range";

    public const string DateTimeOutOfRangeExceptionMessage = "Date and Time is out of range";

    public const string InvalidDateTimeFormatErrorMessage = "Date time format is invalid try: dd-MM-yyyy HH:mm:ss";
}
