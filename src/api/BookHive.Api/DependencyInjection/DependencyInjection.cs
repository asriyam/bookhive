using BookHive.Core.Providers;
using BookHive.Infrastructure.Providers;
using BookHive.Api.Services;

namespace BookHive.Api.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBookHive(this IServiceCollection services)
        {
            // Register provider interfaces with their mock implementations
            services.AddSingleton<IBookProvider, BooksMockProvider>();
            services.AddSingleton<IShelfProvider, ShelvesMockProvider>();
            services.AddSingleton<IUserProvider, UsersMockProvider>();

            // Register business logic services (scoped per HTTP request)
            services.AddScoped<BooksService>();
            services.AddScoped<ShelvesService>();
            services.AddScoped<UsersService>();

            return services;
        }
    }
}
