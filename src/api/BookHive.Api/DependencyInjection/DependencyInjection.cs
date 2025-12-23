using BookHive.Infrastructure.Providers;
using BookHive.Api.Controllers;
using BookHive.Api.Services;

namespace BookHive.Api.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBookHive(this IServiceCollection services)
        {
            services.AddSingleton<BooksMockProvider>();
            services.AddSingleton<ShelvesMockProvider>();
            services.AddSingleton<UsersMockProvider>();

            services.AddScoped<BooksService>();
            services.AddScoped<ShelvesService>();
            services.AddScoped<UsersService>();

            return services;
        }
    }
}
