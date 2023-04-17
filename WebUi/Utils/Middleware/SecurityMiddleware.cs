namespace WebUi.Utils.Middleware;

public class SecurityMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.Headers.Add("X-Xss-Protection", "1");
        context.Response.Headers.Add("X-Frame-Options", "DENY");
        context.Response.Headers.Add("Referrer-Policy", "no-referrer");
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add(
            "Content-Security-Policy",
            "default-src 'self'; " +
            "img-src 'self' http data: https://www.google-analytics.com; " +
            "font-src 'self' data: 'unsafe-inline' https://fonts.googleapis.com https://fonts.gstatic.com; " +
            "style-src 'self' 'unsafe-inline'; " +
            "style-src-elem 'self' 'unsafe-inline' https://fonts.gstatic.com https://fonts.googleapis.com; " +
            "script-src 'self' 'unsafe-eval' 'unsafe-inline' blob: https://www.google-analytics.com;" +
            "frame-src 'self' https://www.youtube.com/embed/ www.youtube.com youtube.com https://app.konsol.pro/ ;" +
            "connect-src http: https: ws: wss:;");

        await _next.Invoke(context);
    }
}