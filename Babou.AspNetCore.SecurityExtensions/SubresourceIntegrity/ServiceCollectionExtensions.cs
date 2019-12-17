using Microsoft.Extensions.DependencyInjection;

namespace Babou.AspNetCore.SecurityExtensions.SubresourceIntegrity
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds required services for Sub-Resource Integrity.
        /// </summary>
        /// <param name="services"></param>
        public static void AddSubresourceIntegrity(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpClient();
        }
    }
}
