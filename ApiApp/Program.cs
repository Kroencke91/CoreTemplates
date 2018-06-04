
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

using ApiApp.Controllers;
using ApiApp.Interfaces;
using ApiApp.Misc;
using Serilog;
using Serilog.Events;

namespace ApiApp
{
    public class Program
    {
        #region Class Variables
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public Methods

        public static int Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                            .Enrich.FromLogContext()
                            .WriteTo.Console(LogEventLevel.Debug)
                            .WriteTo.RollingFile(Path.Combine(Directory.GetCurrentDirectory(), @"Logs\log-{Date}.txt"))
                            .CreateLogger();

            try
            {
                Log.Information($"ApiApp - Starting - {DateTime.Now}");

                var webHost = BuildWebHost(args);

                webHost.Run();

                return 0;
            }
            catch (Exception ex)
            {
                //TODO: Handle Main exception

                Log.Error($"{ex}");

                return -1;
            }
            finally
            {
                Log.Information($"ApiApp - Stopping - {DateTime.Now}");
            }
        }

        #endregion

        #region Private Methods

        private static IWebHost BuildWebHost(string[] args)
        {
            return new WebHostBuilder()
                            .UseKestrel()
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseIISIntegration()
                            .UseStartup<Startup>()
                            .Build();
        }

        #endregion
    }
}
