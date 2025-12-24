namespace Wallet.Api;

public class CorrelationIdMiddleware
{
    private const string CorrelationIdHeader = "X-Correlation-ID";

    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var incomingCorrelationId = context.Request.Headers[CorrelationIdHeader].ToString();
        if (string.IsNullOrWhiteSpace(incomingCorrelationId))
        {
            incomingCorrelationId = Guid.NewGuid().ToString();
        }
        context.Response.Headers.Append(CorrelationIdHeader, incomingCorrelationId);
        using (_logger.BeginScope(new Dictionary<string, object> { { "CorrelationId", incomingCorrelationId } }))
        {
            // _logger.
            
            await _next(context);
        }
        
        
    }
}