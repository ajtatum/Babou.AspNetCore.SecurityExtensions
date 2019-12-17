using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Babou.AspNetCore.SecurityExtensions.XContentTypeOptions
{
    public static partial class AppBuilderExtensions
    {
        /// <summary>
        /// Adds the X-Content-Type-Options header to all responses.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="policy"></param>
        public static void UseXContentTypeOptions(this IApplicationBuilder app, XContentTypeOptions policy = XContentTypeOptions.NoSniff)
        {
            app.UseMiddleware<XContentTypeOptionsMiddleware>();
        }


        internal sealed class XContentTypeOptionsMiddleware
        {
            public XContentTypeOptionsMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            private readonly RequestDelegate _next;

            public async Task Invoke(HttpContext context)
            {
                context.Response.OnStarting(() =>
                {
                    var response = context.Response;
                    response.Headers["X-Content-Type-Options"] = "nosniff";

                    return Task.CompletedTask;
                });

                await _next.Invoke(context);
            }
        }
    }
}
