using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using CommitMaster.Sirius.Domain.Contracts.v1.Mensageria;
using CommitMaster.Sirius.Infra.Data;
using Microsoft.EntityFrameworkCore;
using MassTransit;

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
            services.AddControllersWithViews();
            
            
            services.AddCustomMediator();
            services.AddDbContext<SiriusAppContext>(options =>
            {
                options.UseNpgsql(Configuration["ConnectionStrings:PostgreSqlConnection"], 
                    b => b.MigrationsAssembly("CommitMaster.Sirius.Api"));
            });

            services.AddMassTransit(MassT => {
                MassT.UsingRabbitMq((Context, Configure) => {
                    Configure.Host(new Uri(Configuration["Rabbit:Host"]), host => {
                        host.Username(Configuration["Rabbit:Username"]);
                        host.Password(Configuration["Rabbit:Password"]);
                        host.Heartbeat(ushort.Parse(Configuration["Rabbit:Username"]));
                    });
                    
                    
                    Configure.Message<ISolicitacaoPagamento>(message => {
                        message.SetEntityName("CommitMaster.SolicitacaoPagamento");
                    });

                });
            });
            
            
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CommitMaster.Sirius.Api", Version = "v1" });
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommitMaster.Sirius.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
