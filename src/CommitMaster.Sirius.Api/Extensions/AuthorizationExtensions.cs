using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CommitMaster.Sirius.Api.Extensions;

public static class AuthorizationExtensions
{
    public static IApplicationBuilder UseCustomAuthorization(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }

    public static IServiceCollection AddJwtAuthorization(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration["Secrets:Jwt"]);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }
    
    public static IServiceCollection AddSwaggerWithSecurity(
        this IServiceCollection services)
    {
        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CommitMaster.Sirius.Api", Version = "v1" });
                
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
            { 
                Name = "Authorization", 
                Type = SecuritySchemeType.ApiKey, 
                Scheme = "Bearer", 
                BearerFormat = "JWT", 
                In = ParameterLocation.Header, 
                Description = "Bearer Token", 
            }); 
            c.AddSecurityRequirement(new OpenApiSecurityRequirement 
            { 
                { 
                    new OpenApiSecurityScheme 
                    { 
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme, 
                            Id = "Bearer" 
                        } 
                    }, 
                    new string[] {} 
                } 
            }); 
        });

        return services;
    }
}
