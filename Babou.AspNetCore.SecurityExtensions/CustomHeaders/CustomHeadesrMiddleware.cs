using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Babou.AspNetCore.SecurityExtensions.CustomHeaders
{
    public static partial class AppBuilderExtensions
    {
        public static IApplicationBuilder AddCustomHeaders(this IApplicationBuilder app, string headerName, string headerValue)
        {
            var builder = new CustomHeadersBuilder();
            builder.AddCustomHeader(headerName, headerValue);
            var policy = builder.Build();
            return app.UseMiddleware<SecurityHeadersMiddleware>(policy);
        }

        public static IApplicationBuilder RemoveHeader(this IApplicationBuilder app, string headerName)
        {
            var builder = new CustomHeadersBuilder();
            builder.RemoveHeader(headerName);
            var policy = builder.Build();
            return app.UseMiddleware<SecurityHeadersMiddleware>(policy);
        }

        internal sealed class SecurityHeadersMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly CustomHeadersPolicy _policy;

            public SecurityHeadersMiddleware(RequestDelegate next, CustomHeadersPolicy policy)
            {
                _next = next;
                _policy = policy;
            }

            public async Task Invoke(HttpContext context)
            {
                IHeaderDictionary headers = context.Response.Headers;

                foreach (var headerValuePair in _policy.SetHeaders)
                {
                    headers[headerValuePair.Key] = headerValuePair.Value;
                }

                foreach (var header in _policy.RemoveHeaders)
                {
                    headers.Remove(header);
                }

                await _next(context);
            }
        }
    }
}
