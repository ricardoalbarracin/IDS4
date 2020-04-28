using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace SecurityEncrypt
{
    public static class SecurityEncryptMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityEncrypt(
            this IApplicationBuilder app, List<SecurityEncryptOptions> options)
        {
            //var options = new List<SecurityEncryptOptions>();
            //configureOptions( ref options);
            return app.UseMiddleware<SecurityEncryptMiddleware>(options);
        }
    }
}

