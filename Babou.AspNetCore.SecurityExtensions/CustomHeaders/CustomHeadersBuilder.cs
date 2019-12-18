using System;

namespace Babou.AspNetCore.SecurityExtensions.CustomHeaders
{
    /// <summary>
    /// Exposes methods to build a policy.
    /// </summary>
    public class CustomHeadersBuilder
    {
        private readonly CustomHeadersPolicy _policy = new CustomHeadersPolicy();

        /// <summary>
        /// Adds a custom header to all requests
        /// </summary>
        /// <param name="header">The header name</param>
        /// <param name="value">The value for the header</param>
        /// <returns></returns>
        public CustomHeadersBuilder AddCustomHeader(string header, string value)
        {
            if (string.IsNullOrEmpty(header))
            {
                throw new ArgumentNullException(nameof(header));
            }

            _policy.SetHeaders[header] = value;
            return this;
        }

        /// <summary>
        /// Remove a header from all requests
        /// </summary>
        /// <param name="header">The to remove</param>
        /// <returns></returns>
        public CustomHeadersBuilder RemoveHeader(string header)
        {
            if (string.IsNullOrEmpty(header))
            {
                throw new ArgumentNullException(nameof(header));
            }

            _policy.RemoveHeaders.Add(header);
            return this;
        }

        /// <summary>
        /// Builds a new <see cref="CustomHeadersPolicy"/> using the entries added.
        /// </summary>
        /// <returns>The constructed <see cref="CustomHeadersPolicy"/>.</returns>
        internal CustomHeadersPolicy Build()
        {
            return _policy;
        }
    }
}
