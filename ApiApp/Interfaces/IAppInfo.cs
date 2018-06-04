using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;

namespace ApiApp.Interfaces
{
    public interface IAppInfo
    {
        #region Properties

        IAppConfiguration Config { get; }

        IAppHostingEnvironment Env { get; }

        IApplicationBuilder App { get; }

        IAppSecurity AppSecurity { get; }

        IMemoryCache MemoryCache { get; }

        string EnvironmentName { get; }

        string ApplicationName { get; }

        string LogRootPath { get; }

        #endregion

        #region Public Methods

        IAppInfo AddApp(IApplicationBuilder app);

        IAppInfo AddMemoryCache(IMemoryCache memoryCache);

        #endregion
    }
}
