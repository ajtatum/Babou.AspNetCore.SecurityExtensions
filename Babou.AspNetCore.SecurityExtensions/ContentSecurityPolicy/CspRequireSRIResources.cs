using System;

namespace Babou.AspNetCore.SecurityExtensions.ContentSecurityPolicy
{
    [Flags]
    public enum CspRequireSRIResources
    {
        Style,
        Script,
    }
}
