using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;

using ApiApp.Interfaces;

namespace ApiApp.Misc
{
    public class AppInfo : IAppInfo
    {
        #region Class Variables

        #endregion

        #region Constructors

        public AppInfo(IAppConfiguration config, IAppHostingEnvironment env)
        {
            if (Config != null) throw new ApplicationException("AppInfo should only be instantiated in Startup.");

            Config = config;

            Env = env;
        }

        private AppInfo()
        {
        }

        #endregion

        #region Properties

        public IAppConfiguration Config { get; }

        public IAppHostingEnvironment Env { get; }

        public IApplicationBuilder App { get; private set; }

        public IAppSecurity AppSecurity => Config.AppSecurity;

        public string EnvironmentName => Env.EnvironmentName;

        public string ApplicationName => Env.ApplicationName;

        public string LogRootPath => Config.LogRootPath;

        #endregion

        #region Public Methods

        public IAppInfo AddApp(IApplicationBuilder app)
        {
            App = app;

            return this;
        }

        #endregion

        #region Private Methods
        #endregion
    }
}
