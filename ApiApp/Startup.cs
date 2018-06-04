
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
using Serilog;

using ApiApp.Extensions;
using ApiApp.Interfaces;
using ApiApp.Misc;
using ApiApp.Pipeline;
using ApiApp.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ApiApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using ApiApp.Repositories;

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

        public Startup(IHostingEnvironment env,
                       ILoggerFactory loggerFactory)
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

        public static IAppInfo AppInfo { get; private set; }

        #endregion

        #region Public Methods

        public void ConfigureServices(IServiceCollection services)
        {
            try
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

                //TODO: connection
                var connection = @"Data Source=HPENVYLT2017\MSSQLSERVER2017;Initial Catalog=BVSAFE;Integrated Security=True";

                services.AddDbContext<ValuesContext>(options => options.UseSqlServer(connection))
                        .AddTransient<IValueRepository, ValueRepository>();

                services.AddDbContext<BVContext>(options => options.UseSqlServer(connection))
                        .AddTransient<IBVSafeSiteRepository, BVSafeSiteRepository>();

                services.AddMvc(config =>
                            {
                                config.Filters.Add(typeof(ApiExceptionFilter));
                            });

                services.AddApiVersioning(
                            o =>
                            {
                                o.ReportApiVersions = true;

                                o.AssumeDefaultVersionWhenUnspecified = true;

                                o.DefaultApiVersion = ApiVersionExtensions.CreateApiVersion(CV.ApiVersions.V_1_0);

                                //TODO: Use refelection to add these?
                                o.Conventions.Controller<Controllers.V_1_0.AuthController>().HasApiVersion(ApiVersionExtensions.CreateApiVersion(CV.ApiVersions.V_1_0));

                                o.Conventions.Controller<Controllers.V_1_0.ValuesController>().HasApiVersion(ApiVersionExtensions.CreateApiVersion(CV.ApiVersions.V_1_0));
                            }
                );

                AppInfo = new AppInfo(_config, _env);

                services.AddSingleton(AppInfo);
            }
            catch (Exception ex)
            {
                //TODO: Handle ConfigureServices exception
                throw;
            }
        }

        public void Configure(IApplicationBuilder app, IMemoryCache memoryCache)
        {
            try
            {
                _app = app;

                _app.UseResponseWrapper();

                //_app.UseStatusCodePagesWithReExecute("/Error");

                //if (_config.UseDeveloperExceptionPage)
                //{
                //    _app.UseDeveloperExceptionPage();
                //}
                //else
                //{
                _app.UseExceptionHandler();
                //}

                //_app.UseStatusCodePages();

                _app.UseAuthentication();

                _app.UseMvc();

                AppInfo.AddApp(_app);

                AppInfo.AddMemoryCache(memoryCache);

                ApiControllerBase.InitAppInfo(AppInfo);
            }
            catch (Exception ex)
            {
                //TODO: handle Configure exception
                throw;
            }
        }

        #endregion

        #region Private Methods        
        #endregion
    }
}
