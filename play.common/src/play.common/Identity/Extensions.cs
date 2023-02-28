using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using play.common.Settings;

namespace play.common.Identity
{
    public static class Extensions
    {
        public static AuthenticationBuilder AddJwtBearerAuthentication(this IServiceCollection services)
        {
            return services.ConfigureOptions<ConfigureJwtBearerOptions>()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
        }
    }
}