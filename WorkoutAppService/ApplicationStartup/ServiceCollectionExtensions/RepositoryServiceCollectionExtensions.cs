using Microsoft.Extensions.DependencyInjection;
using WorkoutAppService.Data.Repositories;

namespace WorkoutAppService.ApplicationStartup.ServiceCollectionExtensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            // Interface => concrete implementation
            services.AddScoped<UserRepository>();
            services.AddScoped<MuscleRepository>();
            services.AddScoped<EquipmentRepository>();
            services.AddScoped<ExerciseCategoryRepository>();
            services.AddScoped<ExerciseRepository>();

            return services;
        }
    }
}