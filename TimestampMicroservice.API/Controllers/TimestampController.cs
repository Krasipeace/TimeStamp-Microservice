namespace TimestampMicroservice.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using static TimestampMicroservice.Common.ApiConstants;

    [ApiController]
    [Route("api/[controller]")]
    public class TimestampController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            return Ok(new
            {
                unix = unixTimestamp,
                utc = DateTimeOffset.UtcNow.ToString(DateTimeStringFormat),
                local = DateTimeOffset.Now.ToString(DateTimeStringFormat)
            });
        }

        [HttpGet("{timestamp}")]
        public IActionResult Get(string timestamp)
        {
            if (!long.TryParse(timestamp, out long unixTimestamp))
            {
                return BadRequest(new
                {
                    error = InvalidTimestampInputErrorMessage
                });
            }

            if (unixTimestamp < DateTimeOffset.MinValue.ToUnixTimeSeconds() || unixTimestamp > DateTimeOffset.MaxValue.ToUnixTimeSeconds())
            {
                return BadRequest(new
                {
                    error = TimestampOutOfRangeExceptionMessage
                });
            }

            try
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);

                return Ok(new
                {
                    utc = dateTimeOffset.ToString(DateTimeStringFormat),
                    local = dateTimeOffset.ToLocalTime().ToString(DateTimeStringFormat)
                });
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException($"Timestamp is out of range {ex}");
            }
        }
    }
}
