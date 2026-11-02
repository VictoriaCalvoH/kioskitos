using Kioskitos.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Kioskitos.API.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController(
    HealthCheckService healthCheckService)
    : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(
        typeof(HealthResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(HealthResponse),
        StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<HealthResponse>> Get(
        CancellationToken cancellationToken)
    {
        var report = await healthCheckService.CheckHealthAsync(
            cancellationToken);

        var statusCode = report.Status == HealthStatus.Unhealthy
            ? StatusCodes.Status503ServiceUnavailable
            : StatusCodes.Status200OK;

        return StatusCode(
            statusCode,
            new HealthResponse(report.Status.ToString()));
    }
}