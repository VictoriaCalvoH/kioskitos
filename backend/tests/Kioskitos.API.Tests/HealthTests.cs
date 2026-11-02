using System.Net;
using System.Net.Http.Json;
using Kioskitos.API.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Kioskitos.API.Tests;

public sealed class HealthTests(
    WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetHealth_ReturnsOkAndHealthy()
    {
        using var client = factory.CreateClient();

        using var response = await client.GetAsync("/api/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content
            .ReadFromJsonAsync<HealthResponse>();

        Assert.NotNull(body);
        Assert.Equal("Healthy", body!.Status);
    }
}