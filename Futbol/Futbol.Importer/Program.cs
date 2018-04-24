using System;
using Futbol.Importer.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Futbol.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = DependencyRegistrator.Register();

            var app = serviceProvider.GetService<IApplication>();
            app.Run();
        }
    }
}
