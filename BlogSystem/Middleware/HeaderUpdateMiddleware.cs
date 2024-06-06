

public class HeaderUpdateMiddleware
{
    private readonly RequestDelegate _next;

    public HeaderUpdateMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // IMessageWriter is injected into InvokeAsync
    public async Task InvokeAsync(HttpContext httpContext)
    {
        //svc.Write(DateTime.Now.Ticks.ToString());
        httpContext.Request.Headers.Add("Authorization",  "Bearer "+httpContext.Request.Cookies["token"]);
        string token = httpContext.Request.Headers["Authorization"].ToString();
        Console.WriteLine("called");
        await _next(httpContext);
    }
}

public static class MyCustomMiddlewareExtensions
{
    public static IApplicationBuilder UseHeaderUpdateMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HeaderUpdateMiddleware>();
    }
}