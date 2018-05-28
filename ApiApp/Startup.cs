
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using ApiApp.Interfaces;
using ApiApp.Misc;
using ApiApp.Pipeline;
using ApiApp.Controllers;
using Microsoft.Extensions.Caching.Memory;

namespace ApiApp
{
    public class Startup
    {
        #region Class Variables

        private IAppConfiguration _config;

        private IAppHostingEnvironment _env;

        private static IApplicationBuilder _app;

        #endregion

        #region Constructors

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                                    .SetBasePath(env.ContentRootPath)
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                                    .AddEnvironmentVariables()
                            ;

            env.EnvironmentName = new AppConfiguration(builder.Build()).EnvironmentName;

            builder.AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.json", optional: false, reloadOnChange: false);

            _config = new AppConfiguration(builder.Build());

            _env = new AppHostingEnvironment(env);
        }

        #endregion

        #region Properties
        #endregion

        #region Public Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                ValidateIssuer = true,
                                IssuerSigningKey = _config.AppSecurity.IssuerSigningKey,
                                ValidIssuer = _config.JwtInfo.Issuer,
                                ValidAudience = _config.JwtInfo.Audience
                            };
                        }
                );

            services.AddMemoryCache();

            services.AddMvc();

            services.AddApiVersioning();

            services.AddSingleton<IAppInfo>(new AppInfo(_config, _env));
        }

        public void Configure(IApplicationBuilder app, IMemoryCache memoryCache)
        {
            _app = app;

            if (_config.UseDeveloperExceptionPage)
            {
                _app.UseDeveloperExceptionPage();
            }
            else
            {
                _app.UseExceptionHandler();
            }

            //_app.UseStatusCodePages();

            _app.UseMiddleware<ResponseWrapper>();

            _app.UseAuthentication();

            _app.UseMvc();

            _app.ApplicationServices.GetService<IAppInfo>().AddApp(_app);

            ControllerBase.InitMemoryCache(memoryCache);

            ControllerBase.InitAppInfo(_app.ApplicationServices.GetService<IAppInfo>());
        }

        #endregion

        #region Private Methods        
        #endregion
    }
}
