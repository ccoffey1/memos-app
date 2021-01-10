using JWTTest.Repositories;
using JWTTest.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JWTTest
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
