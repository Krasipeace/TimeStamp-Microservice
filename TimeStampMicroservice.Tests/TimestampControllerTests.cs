namespace TimestampMicroservice.Tests;

using System;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using TimestampMicroservice.API.Controllers;

public class TimestampControllerTests
{
    private readonly TimestampController apiTestController;

    public TimestampControllerTests()
    {
        apiTestController = new TimestampController();
    }

    [Fact]
    public void Get_ReturnsOkResult_WithCurrentTimestamp()
    {
        var okResult = apiTestController.Get() as OkObjectResult;

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void Get_WithValidTimestamp_ReturnsOkResult()
    {
        string timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

        var okResult = apiTestController.Get(timestamp) as OkObjectResult;

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void Get_WithInvalidTimestamp_ReturnsBadRequest()
    {
        string timestamp = "invalid_timestamp";

        var badRequestResult = apiTestController.Get(timestamp) as BadRequestObjectResult;

        Assert.NotNull(badRequestResult);
        Assert.Equal(400, badRequestResult.StatusCode);

        string invalidDate = "2022-13-45";
        badRequestResult = apiTestController.Get(invalidDate) as BadRequestObjectResult;
        Assert.NotNull(badRequestResult);
        Assert.Equal(400, badRequestResult.StatusCode);

        string invalidString = "abc123";
        badRequestResult = apiTestController.Get(invalidString) as BadRequestObjectResult;
        Assert.NotNull(badRequestResult);
        Assert.Equal(400, badRequestResult.StatusCode);

        string timestampOutOfRange = "253402300800";
        badRequestResult = apiTestController.Get(timestampOutOfRange) as BadRequestObjectResult;
        Assert.NotNull(badRequestResult);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
}
