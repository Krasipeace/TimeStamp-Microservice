namespace TimestampMicroservice.App.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TimeStampViewModel : HumanDateTimeViewModel
    {
        [Range(0, long.MaxValue)]
        public long Unix { get; set; }
    }
}
