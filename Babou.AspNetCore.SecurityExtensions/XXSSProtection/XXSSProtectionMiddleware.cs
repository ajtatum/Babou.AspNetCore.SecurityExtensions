using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Babou.AspNetCore.SecurityExtensions.XXSSProtection
{
    public static partial class AppBuilderExtensions
    {
        /// <summary>
        /// Adds the X-XSS-Protection header to each response with text/html media type.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        public static void UseXXSSProtection(this IApplicationBuilder app, XXSSProtectionOptions options)
        {
            app.UseMiddleware<XXSSProtectionMiddleware>(options);
        }

        /// <summary>
        /// Adds the X-XSS-Protection header to each response with text/html media type.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="enabled">Enables XSS protection.</param>
        /// <param name="block">Sets Block mode.</param>
        /// <param name="reportUri">Sets the URI the browser is going to report violations to.</param>
        public static void UseXXSSProtection(this IApplicationBuilder app, bool enabled = true, bool block = true, Uri? reportUri = null)
        {
            var options = new XXSSProtectionOptions()
            {
                Enabled = enabled,
                Block = block,
                ReportUri = reportUri
            };

            app.UseMiddleware<XXSSProtectionMiddleware>(options);
        }


        internal sealed class XXSSProtectionMiddleware
        {
            public XXSSProtectionMiddleware(RequestDelegate next, XXSSProtectionOptions options)
            {
                _next = next;
                Options = options ?? throw new ArgumentNullException(nameof(options));
                _headerValue = Options.ToString();
            }

            private readonly RequestDelegate _next;
            private readonly string _headerValue;

            public XXSSProtectionOptions Options { get; }
            
            public async Task Invoke(HttpContext context)
            {
                context.Response.OnStarting(() =>
                {
                    var response = context.Response;

                    if (response.GetTypedHeaders().ContentType?.MediaType.Equals("text/html", StringComparison.OrdinalIgnoreCase) ?? false)
                    {
                        response.Headers["X-XSS-Protection"] = _headerValue;
                    }

                    return Task.CompletedTask;
                });

                await _next.Invoke(context);
            }
        }
    }
}
