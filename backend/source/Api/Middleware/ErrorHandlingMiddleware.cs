using System.Text.Json;
using Api.Errors;
using Client;
using FluentValidation;
using ILogger = Serilog.ILogger;
using ResponseError = Api.Errors.ResponseError;

namespace Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (ResponseError ex)
        {
            await HandleResponseErrors(httpContext, ex);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptions(httpContext, ex);
        }
        catch (Exception ex)
        {
            await HandleInternalErrors(httpContext, ex);
        }
    }

    private static async Task HandleInternalErrors(HttpContext httpContext, Exception exception)
    {
        SetContentTypeToJson(httpContext);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await SerializeAndWriteResponse(httpContext, new ErrorResponse(exception.Message));
    }

    private async Task HandleValidationExceptions(HttpContext httpContext, ValidationException exception)
    {
        SetContentTypeToJson(httpContext);
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        var error = ProcessValidationException(exception);
        await SerializeAndWriteResponse(httpContext, error);
    }

    private async Task HandleResponseErrors(HttpContext httpContext, ResponseError exception)
    {
        SetContentTypeToJson(httpContext);
        var error = ProcessException(exception, httpContext.Response);
        await SerializeAndWriteResponse(httpContext, error);
    }

    private static async Task SerializeAndWriteResponse(HttpContext httpContext, ErrorResponse errorResponse)
    {
        var result = JsonSerializer.Serialize(errorResponse, JsonSerializerOptions.Default);
        await httpContext.Response.WriteAsync(result);
    }

    private static void SetContentTypeToJson(HttpContext httpContext)
        => httpContext.Response.ContentType = "application/json";


    private ErrorResponse ProcessValidationException(ValidationException ex)
    {
        return new ErrorResponse(ex.Errors.Select(x => x.ErrorMessage));
    }

    private ErrorResponse ProcessException(ResponseError ex, HttpResponse response)
    {
        switch (ex)
        {
            case NotFoundError notFoundError:
                logger.Error(notFoundError, notFoundError.Message);
                response.StatusCode = notFoundError.StatusCode;
                break;
            case ForbiddenError forbiddenError:
                logger.Error(forbiddenError, forbiddenError.Message);
                response.StatusCode = forbiddenError.StatusCode;
                break;
            case BadRequestError badRequestError:
                logger.Error(badRequestError, badRequestError.Message);
                response.StatusCode = badRequestError.StatusCode;
                break;
            default:
                logger.Error(ex, "Unknown Exception - {Error}", ex.Message);
                response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        return new ErrorResponse(ex.Message.Split(ResponseError.MessageSeparator));
    }
}