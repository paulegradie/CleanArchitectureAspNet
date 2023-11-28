using Api.Domain;
using ILogger = Serilog.ILogger;

namespace Api.Middleware;

public class UnitOfWorkMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger logger;

    public UnitOfWorkMiddleware(RequestDelegate next, ILogger logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, AppDbContext dbContext)
    {
        try
        {
            await next(httpContext);
            await dbContext.SaveChangesAsync(httpContext.RequestAborted);
        }
        catch (Exception ex)
        {
            // do not save changes
            logger.Error(ex, "Error encountered");
            throw;
        }
    }
}