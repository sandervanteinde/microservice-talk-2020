using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSharedAppParts(this IApplicationBuilder app, string apiName)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", apiName);
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
