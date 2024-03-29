﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Futbol.Common.Infrastructure;
using Futbol.Importer.Repositories.Interfaces;
using Futbol.Importer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Futbol.Importer.Infrastructure
{
    public static class DependencyRegistrator
    {
        public static IServiceProvider Register()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            var settings = new Configuration();
            configuration.Bind(settings);
            services.Configure<Configuration>(configuration);
            services.AddScoped(x => x.GetService<IOptionsSnapshot<Configuration>>().Value);

            Dictionary<string, string> connStrs = new Dictionary<string, string>();

            services.AddDbContext<FutbolContext>(options => options.UseSqlServer(settings.ConnectionStrings.FutbolDbConnection));

            services.AddSingleton<IApplication, Application>();
            services.Scan(s => s.FromAssemblyOf<IRepository>().AddClasses(c => c.AssignableTo<IRepository>()).AsImplementedInterfaces().WithTransientLifetime());
            services.Scan(s => s.FromAssemblyOf<IService>().AddClasses(c => c.AssignableTo<IService>()).AsImplementedInterfaces().WithTransientLifetime());

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}

