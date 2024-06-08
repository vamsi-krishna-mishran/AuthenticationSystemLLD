

using Microsoft.Extensions.Primitives;

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
        try
        {
            httpContext.Request.Headers.Add("Authorization", "Bearer " + httpContext.Request.Cookies["token"]);
            string token = httpContext.Request.Headers["Authorization"].ToString();
            //httpContext.Response.Headers.Add('Access-Control-Allow-Origin',;
            Console.WriteLine("called");
            await _next(httpContext);
        }
        catch(Exception ex)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.ContentType = "text/plain";   //add this line.....

            await httpContext.Response.WriteAsync("login first");
            return;
        }
        
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