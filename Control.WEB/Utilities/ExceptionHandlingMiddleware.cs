namespace Control.WEB.Utilities;

public class ExceptionHandlingMiddleware : IMiddleware
{
    #region Own fields

    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    #endregion

    #region Ctor

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger= logger;
    }

    #endregion

    #region Methods

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            _logger.LogInformation($"{context.GetEndpoint()} Request is successfull");
            await next(context);
        }

        catch (InvalidValueException ex)
        {
            context.Response.StatusCode=(int)HttpStatusCode.Conflict;
            _logger.LogError($"{context.GetEndpoint()} {ex.Message}");
            await context.Response.WriteAsync(ex.Message);
        }

        catch (ObjectNotFoundException ex)
        {
            context.Response.StatusCode=(int)HttpStatusCode.NotFound;
            _logger.LogError($"{context.GetEndpoint()} {ex.Message}");
            await context.Response.WriteAsync(ex.Message);
        }

        catch(Exception ex)
        {
            context.Response.StatusCode=(int)HttpStatusCode.BadRequest;
            _logger.LogError($"{context.GetEndpoint()} {ex.Message}");
            await context.Response.WriteAsync(ex.Message);
        }
    }

    #endregion
}