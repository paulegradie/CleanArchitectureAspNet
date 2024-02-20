using Persistence.Abstractions;

namespace Api.Middleware;

public class UnitOfWorkMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<UnitOfWorkMiddleware> logger;

    public UnitOfWorkMiddleware(RequestDelegate next, ILogger<UnitOfWorkMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, IUnitOfWork unitOfWork)
    {
        try
        {
            await next(httpContext);
            await unitOfWork.SaveChangesAsync(httpContext.RequestAborted);
        }
        catch (Exception ex)
        {
            // do not save changes
            logger.Log(LogLevel.Error, ex, "Error closing out the unit of work. No changes saved...");
            throw;
        }
    }
}