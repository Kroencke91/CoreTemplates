
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApp.Misc;
using Microsoft.Extensions.Configuration;

namespace ApiApp.Interfaces
{
    public interface IAppConfiguration : IConfiguration
    {
        #region Properties

        string EnvironmentName { get; }

        bool UseDeveloperExceptionPage { get; }

        string LogRootPath { get; }

        JwtInfo JwtInfo { get; }

        IAppSecurity AppSecurity { get; }

        #endregion

        #region Public Methods
        #endregion
    }
}
