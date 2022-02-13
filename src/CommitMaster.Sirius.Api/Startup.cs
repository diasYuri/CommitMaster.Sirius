using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommitMaster.Sirius.Api.Extensions;
using CommitMaster.Sirius.Api.HostedServices;
using CommitMaster.Sirius.Api.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using CommitMaster.Sirius.App.Extensions;
using CommitMaster.Sirius.Infra.Autentication;
using CommitMaster.Sirius.Infra.Criptografia.v1;
using CommitMaster.Sirius.Infra.CrossCutting;
using CommitMaster.Sirius.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace CommitMaster.Sirius.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddJwtAuthorization(Configuration);
            
            
            services.AddCustomMediator();
            services.AddDbContext<SiriusAppContext>(options =>
            {
                options.UseNpgsql(Configuration["ConnectionStrings:PostgreSqlConnection"], 
                    b => b.MigrationsAssembly("CommitMaster.Sirius.Api"));
            });


            services.AddMessageBus(Configuration["ConnectionStrings:RabbitMQ"])
                .AddHostedService<PaymentServiceCallback>();
            
            
            services
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IPasswordEncrypt, PasswordEncrypt>()
                .AddScoped<UserAccessor>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddSwaggerWithSecurity();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommitMaster.Sirius.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            app.UseRouting();

            app.UseCustomAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
