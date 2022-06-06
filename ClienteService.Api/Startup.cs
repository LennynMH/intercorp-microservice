using ClienteService.Core.Interfaces;
using ClienteService.Core.Options;
using ClienteService.Infrastructure.Filters;
using ClienteService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace ClienteService.Api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        #region INJECTIONS
        public IConfiguration Configuration { get; }
        #endregion

        #region CONSTRUCTOR
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region METHODS
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            })
            .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClienteService.Api", Version = "v1" });
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.Configure<ConnectionOptions>(Configuration.GetSection("ConnectionOptions"));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IClienteService, Core.Services.ClienteService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(MyAllowSpecificOrigins);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ClienteService.Api v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        #endregion
    }
}
