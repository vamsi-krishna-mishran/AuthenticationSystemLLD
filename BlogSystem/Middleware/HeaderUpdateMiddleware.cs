

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
            var token = httpContext?.Request?.Cookies?["token"];
            if (token == null || string.IsNullOrEmpty(token))
            {
                // Handle the case where the token is missing or empty
                token = "NONE";
            }

            // Add the Authorization header only if the token is not null or empty
            //if (!string.IsNullOrEmpty(token))
            {
                Console.WriteLine("___________");
                Console.WriteLine(token);
                Console.WriteLine("___________");
                httpContext.Request.Headers.Add("Authorization", "Bearer " + token);
                await _next(httpContext);
            }
        }
        catch(Exception ex)
        {
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "text/plain";   //add this line.....
            Console.WriteLine("********HeaderUpdateMiddlware2*********");
            await httpContext.Response.WriteAsync(ex.Message);
            Console.Write(ex.ToString());
            Console.WriteLine(ex.Message);
            Console.WriteLine("********HeaderUpdateMiddlware2*********");
            //await _next(httpContext);
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