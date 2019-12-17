using System;

namespace Babou.AspNetCore.SecurityExtensions.ContentSecurityPolicy
{
    [Flags]
    public enum CspSandboxRules
    {
        Sandbox = 0,

        AllowForms,
        AllowSameOrigin,
        AllowScripts,
        AllowPopups,
        AllowModals,
        AllowOrientationLock,
        AllowPointerLock,
        AllowPresentation,
        AllowPopupsToEscapeSandbox,
        AllowTopNavigation,
    }
}
