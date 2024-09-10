using FleetHQ.Server.Helpers;

using Microsoft.AspNetCore.Diagnostics;

namespace FleetHQ.Server.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        await httpContext.Response.WriteAsJsonAsync(
            XResult.Exception(exception.Message), cancellationToken: cancellationToken);

        return true;
    }
}