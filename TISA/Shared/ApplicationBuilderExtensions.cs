using Microsoft.AspNetCore.Builder;

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
