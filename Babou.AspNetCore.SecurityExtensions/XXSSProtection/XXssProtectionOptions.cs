using System;
using System.Text;

namespace Babou.AspNetCore.SecurityExtensions.XXSSProtection
{
    public class XXSSProtectionOptions
    {
        /// <summary>
        /// Gets or sets whether XSS protection is enabled.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool Block { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public Uri? ReportUri { get; set; }


        public override string ToString()
        {
            var builder = new StringBuilder();

            if (this.Enabled)
            {
                builder.Append("1");

                if (this.Block)
                    builder.Append("; mode=block");

                if (this.ReportUri != null)
                    builder.Append($"report={this.ReportUri}");
            }
            else
                builder.Append("0");

            return builder.ToString();
        }
    }
}
