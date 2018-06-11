using System.Linq;
using Futbol.API.Middleware;
using Futbol.API.Repositories.Interfaces;
using Futbol.API.Services.Interfaces;
using Futbol.Common.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Futbol.API
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            builder.AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Configures the services the application needs
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
                c.SwaggerDoc("v1", new Info { Title = "Futbol API", Version = "v1" });
                c.IncludeXmlComments($"{basePath}\\Futbol.API.xml");
            });

            services.Scan(s => s.FromAssemblyOf<IRepository>().AddClasses(c => c.AssignableTo<IRepository>()).AsImplementedInterfaces().WithTransientLifetime());
            services.Scan(s => s.FromAssemblyOf<IService>().AddClasses(c => c.AssignableTo<IService>()).AsImplementedInterfaces().WithTransientLifetime());

            services.AddDbContext<FutbolContext>(options => options.UseSqlServer(this.Configuration["ConnectionString:FutbolDbConnection"]));
            services.AddSingleton<IConfigurationRoot>(this.Configuration);
        }

        /// <summary>
        /// Configures the specified application itself.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The environment</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FUTBOL API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMiddleware(typeof(ErrorHandlerMiddleware));

            app.UseMvc();
        }
    }
}
