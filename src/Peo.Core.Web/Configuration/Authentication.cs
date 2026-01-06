using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Peo.Core.Dtos;
using System.Text;

namespace Peo.Core.Web.Configuration
{
    public static class Authentication
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        var jwtSettings = configuration.GetSection(JwtSettings.Position).Get<JwtSettings>();

                        if (jwtSettings is null)
                            throw new InvalidOperationException("Seção Jwt não encontrada. Configure Jwt__Key, Jwt__Issuer e Jwt__Audience via variáveis de ambiente/Secret/ConfigMap.");

                        if (string.IsNullOrWhiteSpace(jwtSettings.Key))
                            throw new InvalidOperationException("JWT Key vazia. Configure Jwt__Key via Secret/variável de ambiente.");

                        if (string.IsNullOrWhiteSpace(jwtSettings.Issuer) || string.IsNullOrWhiteSpace(jwtSettings.Audience))
                            throw new InvalidOperationException("JWT Issuer/Audience vazios. Configure Jwt__Issuer e Jwt__Audience via ConfigMap/variável de ambiente.");

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtSettings.Issuer,
                            ValidAudience = jwtSettings.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                        };
                    });

            return services;
        }
    }
}