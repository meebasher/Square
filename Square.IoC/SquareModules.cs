using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Square.Api.Data.Interfaces;
using Square.Infra.Data.Repositories;
using System.Reflection;

namespace Square.IoC
{
    public static class SquareModules
    {
        public static void Register(this IServiceCollection services)
        {
            //services
            services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //data
            services.AddScoped<IPointRepository, PointRepository>();
        }

        public static void AddDbContextGeneric<T>(this IServiceCollection services, string connectionStringName) where T : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>() ??
                throw new ArgumentNullException("serviceProvider configuration");

            services.AddDbContext<T>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(connectionStringName));
            });
        }

        public static async Task MigrateDbContextGenericAsync<T>(this IServiceProvider serviceProvider) where T : DbContext
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<T>();

#if DEBUG
            await db.Database.EnsureDeletedAsync();
#endif
            await db.Database.MigrateAsync();
        }
    }
}
