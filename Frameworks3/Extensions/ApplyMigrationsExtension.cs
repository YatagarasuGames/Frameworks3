using Frameworks3.Models;
using Microsoft.EntityFrameworkCore;

namespace Frameworks3.Extensions
{
    public static class ApplyMigrationsExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<Context>();
                context!.Database.Migrate();
            }
        }
    }
}
