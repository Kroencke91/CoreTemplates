﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Serilog;

namespace ApiApp.Interfaces
{
    public interface IAppHostingEnvironment
    {
        #region Properties

        string EnvironmentName { get; }

        string ApplicationName { get; }

        string WebRootPath { get; }

        IFileProvider WebRootFileProvider { get; }

        string ContentRootPath { get; }

        IFileProvider ContentRootFileProvider { get; }

        ILogger Log { get; }

        #endregion

        #region Methods

        #endregion
    }
}
