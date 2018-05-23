
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace ApiApp.Interfaces
{
    public interface IAppConfiguration : IConfiguration
    {
        #region Properties

        string EnvironmentName { get; }

        string AppEnvironment { get; }

        bool UseDeveloperExceptionPage { get; }

        string LogDir { get; }

        #endregion

        #region Public Methods
        #endregion
    }
}
