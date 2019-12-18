using System.Collections.Generic;

namespace Babou.AspNetCore.SecurityExtensions.CustomHeaders
{
    public class CustomHeadersPolicy
    {
        public IDictionary<string, string> SetHeaders { get; } = new Dictionary<string, string>();
        public ISet<string> RemoveHeaders { get; } = new HashSet<string>();
    }
}
