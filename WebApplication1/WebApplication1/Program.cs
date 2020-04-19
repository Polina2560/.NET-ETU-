using BL.Interfaces;
using DL;
using DL.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<EFDbContext>();
                    SampleData.InitData(context);
            }
            host.Run();
        }


        public static IWebHost BuildWebHost(string[] args) =>
WebHost.CreateDefaultBuilder(args)
.UseStartup<Startup>()
.Build();
    }
}
