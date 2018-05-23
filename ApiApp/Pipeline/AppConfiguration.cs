
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

using ApiApp.Interfaces;
using ApiApp.Misc;

namespace ApiApp.Pipeline
{
    public class AppConfiguration : IAppConfiguration
    {
        #region Class Variables
        #endregion

        #region Constructors

        public AppConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;

            InitializeAppConfig();
        }

        #endregion

        #region Properties

        private IConfiguration Configuration { get; }

        public string EnvironmentName { get; private set; }

        public bool UseDeveloperExceptionPage { get; private set; }

        public string LogRootPath { get; private set; }

        public JwtInfo JwtInfo { get; private set; }

        public IAppSecurity AppSecurity { get; private set; }

        #region IConfiguration Properties

        //
        // Summary:
        //     Gets or sets a configuration value.
        //
        // Parameters:
        //   key:
        //     The configuration key.
        //
        // Returns:
        //     The configuration value.
        public string this[string key] { get { return Configuration[key]; } set { Configuration[key] = value; } }

        #endregion

        #endregion

        #region Public Methods

        #region IConfiguration Public Methods

        //
        // Summary:
        //     Gets the immediate descendant configuration sub-sections.
        //
        // Returns:
        //     The configuration sub-sections.
        public IEnumerable<IConfigurationSection> GetChildren() => Configuration.GetChildren();

        //
        // Summary:
        //     Returns a Microsoft.Extensions.Primitives.IChangeToken that can be used to observe
        //     when this configuration is reloaded.
        //
        // Returns:
        //     A Microsoft.Extensions.Primitives.IChangeToken.
        public IChangeToken GetReloadToken() => Configuration.GetReloadToken();

        //
        // Summary:
        //     Gets a configuration sub-section with the specified key.
        //
        // Parameters:
        //   key:
        //     The key of the configuration section.
        //
        // Returns:
        //     The Microsoft.Extensions.Configuration.IConfigurationSection.
        //
        // Remarks:
        //     This method will never return null. If no matching sub-section is found with
        //     the specified key, an empty Microsoft.Extensions.Configuration.IConfigurationSection
        //     will be returned.
        public IConfigurationSection GetSection(string key) => Configuration.GetSection(key);

        #endregion

        #endregion

        #region Private Methods

        private void InitializeAppConfig()
        {
            EnvironmentName = Configuration["Startup:EnvironmentName"];

            UseDeveloperExceptionPage = Convert.ToBoolean(Configuration["StartUp:UseDeveloperExceptionPage"]);

            LogRootPath = Configuration["LogRootPath"];

            var issuer = Configuration["JWT:Issuer"];

            var audience = Configuration["JWT:Audience"];

            JwtInfo = new JwtInfo(issuer, audience);

            AppSecurity = new AppSecurity(this);
        }

        #endregion
    }
}
