using System.Text;
using WebUi.Utils.Middleware;

namespace WebUi.Utils.Extensions
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseSecurity(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecurityMiddleware>();
            return app;
        }

        public static IApplicationBuilder UseRegisteredServicesPage(this IApplicationBuilder app, IServiceCollection services, string endpoint = "/asp_services")
        {
            app.Map(endpoint, builder => builder.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>Registered Services</h1>");
                sb.Append("<table><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                await context.Response.WriteAsync(sb.ToString());
            }));

            return app;
        }


    }
}