namespace TimestampMicroservice.API.Controllers;

using Microsoft.AspNetCore.Mvc;

using System.Globalization;

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
            throw new ArgumentOutOfRangeException($"{TimestampOutOfRangeExceptionMessage} {ex}");
        }
    }

    [HttpPost("{dateTime}")]
    public IActionResult ConvertDateTime(string dateTime)
    {
        if (!DateTime.TryParseExact(dateTime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            return BadRequest(new
            {
                error = InvalidDateTimeFormatErrorMessage
            });
        }

        if (parsedDate < DateTime.MinValue || parsedDate > DateTime.MaxValue)
        {
            return BadRequest(new
            {
                error = DateTimeOutOfRangeExceptionMessage
            });
        }

        string formattedDate = parsedDate.ToString("dd-MM-yyyy HH:mm:ss");
        DateTime finalDate = DateTime.ParseExact(formattedDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

        finalDate = DateTime.SpecifyKind(finalDate, DateTimeKind.Utc);
        finalDate = finalDate.ToOADate() < 0 ? DateTime.MinValue : finalDate;

        var timestampOffset = new DateTimeOffset(finalDate).ToUnixTimeSeconds();
        var timestampOffsetLocal = new DateTimeOffset(finalDate).ToLocalTime().ToUnixTimeSeconds();

        try
        {
            return Ok(new
            {
                timestamp = timestampOffset,
                timestampLocal = timestampOffsetLocal
            });
        }
        catch (Exception ex)
        {
            throw new ArgumentOutOfRangeException($"{DateTimeOutOfRangeExceptionMessage} {ex}");
        }
    }
}
