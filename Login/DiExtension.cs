using Application.Member;

namespace Login
{
    /**
    要安裝 
    Microsoft.Extensions.DependencyInjection.Abstractions.dll
    */

    public static class DiExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMemberRepository, EFMemberRepository>();

            return services;
        }
    }
}
