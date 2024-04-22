namespace TimestampMicroservice.Common
{
    public static class AppConstants
    {
        public const int ReleaseYear = 2024;

        public const int InputMaxLength = 60;
        public const int InputMinLength = 8;

        public const string ApiUrl = "https://localhost:44340/Timestamp";
        public const string TimeStampApiUrl = "https://localhost:44340/Timestamp/";

        public const string DockerApiUrl = "https://localhost:32779/Timestamp";
        public const string DockerTimeStampApiUrl = "https://localhost:32779/Timestamp/";

        public const string InvalidResponseExceptionMessage = "Cannot retrieve timestamp from the server.";
    }
}
