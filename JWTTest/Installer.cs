using MemoApp.Repositories;
using MemoApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MemoApp
{
    public static class Installer
    {
        public static void AddServicesAndRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
