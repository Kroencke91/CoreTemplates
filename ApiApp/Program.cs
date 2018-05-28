
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using ApiApp.Controllers;
using ApiApp.Interfaces;
using ApiApp.Misc;

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
            var webHost = BuildWebHost(args);

            webHost.Run();

            return 0;
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
