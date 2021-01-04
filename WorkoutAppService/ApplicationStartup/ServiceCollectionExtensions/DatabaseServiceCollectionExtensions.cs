using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkoutAppService.Data;
using WorkoutAppService.Models.Settings;

namespace WorkoutAppService.ApplicationStartup.ServiceCollectionExtensions
{
    public static class DatabaseServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MySQLSettings>(config.GetSection("MySQL"));

            var settings = config.GetSection("MySQL").Get<MySQLSettings>();

            services.AddDbContext<DataContext>(
                dbContextOptions =>
                {
                    dbContextOptions
                        .UseMySql(settings.DefaultConnection, ServerVersion.AutoDetect(settings.DefaultConnection), options =>
                        {
                            options.EnableRetryOnFailure();
                            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });

                    if (settings.EnableDetailedErrors)
                    {
                        dbContextOptions.EnableDetailedErrors();
                    }

                    if (settings.EnableSensitiveDataLogging)
                    {
                        dbContextOptions.EnableSensitiveDataLogging();
                    }
                }
            );

            services.AddTransient<Seeder>();

            return services;
        }
    }
}