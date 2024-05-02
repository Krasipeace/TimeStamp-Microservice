namespace TimestampMicroservice.Tests;

using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Newtonsoft.Json;
using Moq.Protected;
using TimestampMicroservice.App.Controllers;
using TimestampMicroservice.App.Models;
using TimestampMicroservice.App.Services.Contracts;
using TimestampMicroservice.App.Services;

public class TimestampServiceTests
{
    private readonly Mock<ITimestampService> mockTimeService;
    private readonly TimestampController controller;
    private readonly Mock<HttpMessageHandler> mockHttpMessageHandler;
    private readonly TimestampService service;

    public TimestampServiceTests()
    {
        mockTimeService = new Mock<ITimestampService>();
        controller = new TimestampController(mockTimeService.Object);
        mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        service = new TimestampService(httpClient);
    }

    [Fact]
    public async Task GetCurrent_ReturnsCorrectTimeStampViewModel()
    {
        var expectedModel = new TimeStampViewModel
        {
            Unix = 1714324986,
            Utc = "28-04-2024 17:23:06",
            Local = "28-04-2024 20:23:06"
        };

        mockTimeService.Setup(x => x.GetCurrentTimeAsync()).ReturnsAsync(expectedModel);

        var result = await controller.GetCurrent();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<TimeStampViewModel>(viewResult.ViewData.Model);
        Assert.Equal(expectedModel.Unix, model.Unix);
        Assert.Equal(expectedModel.Utc, model.Utc);
        Assert.Equal(expectedModel.Local, model.Local);
    }

    [Fact]
    public void GetDateTime_ReturnsCorrectViewResult()
    {
        var result = controller.GetDateTime();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task GetDateTimeAsync_ReturnsCorrectHumanDateTimeViewModel()
    {
        var expectedModel = new HumanDateTimeViewModel
        {
            Utc = "28-04-2024 17:23:06",
            Local = "28-04-2024 20:23:06"
        };

        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(expectedModel), Encoding.UTF8, "application/json"),
        };

        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        var result = await service.GetDateTimeAsync("1714324986");

        Assert.Equal(expectedModel.Utc, result.Utc);
        Assert.Equal(expectedModel.Local, result.Local);
    }

    [Fact]
    public async Task GetDateTime_ReturnsCorrectViewResultWithHumanDateTimeViewModel()
    {
        var expectedModel = new HumanDateTimeViewModel 
        { 
            Utc = "28-04-2024 17:23:06", 
            Local = "28-04-2024 20:23:06" 
        };
        mockTimeService.Setup(x => x.GetDateTimeAsync(It.IsAny<string>())).ReturnsAsync(expectedModel);

        var result = await controller.GetDateTime("1714324986");

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<HumanDateTimeViewModel>(viewResult.ViewData.Model);
        Assert.Equal(expectedModel.Utc, model.Utc);
        Assert.Equal(expectedModel.Local, model.Local);
    }

    [Fact]
    public async Task GetDateTime_ReturnsBadRequest_WhenTimestampIsOutOfRange()
    {
        var expectedModel = new HumanDateTimeViewModel
        {
            Utc = "28-04-9999 17:23:20",
            Local = "28-04-9999 20:23:20"
        };

        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest,
            Content = new StringContent(JsonConvert.SerializeObject(expectedModel), Encoding.UTF8, "application/json"),
        };

        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        var result = await service.GetDateTimeAsync("253402300800");

        Assert.Equal(expectedModel.Utc, result.Utc);
        Assert.Equal(expectedModel.Local, result.Local);
    }
}
