using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Babou.AspNetCore.SecurityExtensions.XDownloadOptions
{
    public static partial class AppBuilderExtensions
    {
        /// <summary>
        /// Adds the X-Download-Options header with 'noopen' value to all file downloads.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        public static void UseXDownloadOptions(this IApplicationBuilder app, XDownloadOptions options = XDownloadOptions.NoOpen)
        {
            app.UseMiddleware<XDownloadOptionsMiddleware>();
        }


        internal sealed class XDownloadOptionsMiddleware
        {
            public XDownloadOptionsMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            private readonly RequestDelegate _next;

            public XDownloadOptions Mode { get; } = default(XDownloadOptions);

            public async Task Invoke(HttpContext context)
            {
                context.Response.OnStarting(() =>
                {
                    var response = context.Response;

                    if (response.GetTypedHeaders().ContentDisposition?.DispositionType.Equals("attachment", StringComparison.OrdinalIgnoreCase) ?? false)
                    {
                        response.Headers["X-Download-Options"] = "noopen";
                    }

                    return Task.CompletedTask;
                });

                await _next.Invoke(context);
            }
        }
    }
}
