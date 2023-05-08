using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
<<<<<<< HEAD
=======
using Serilog;
>>>>>>> origin/remove-daos

namespace Codecool.CodecoolShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
<<<<<<< HEAD
=======
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();



            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            try
            {
                Log.Information("App is starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "App failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
>>>>>>> origin/remove-daos
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
<<<<<<< HEAD
=======
                .UseSerilog()
>>>>>>> origin/remove-daos
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
<<<<<<< HEAD
=======
        


>>>>>>> origin/remove-daos
    }
}
