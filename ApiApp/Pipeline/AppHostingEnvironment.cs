using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

using ApiApp.Interfaces;

namespace ApiApp.Pipeline
{
    public class AppHostingEnvironment : IAppHostingEnvironment
    {
        #region Class Variables
        #endregion

        #region Constructors

        public AppHostingEnvironment(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;

            InitializeHostingEnvironment();
        }

        #endregion

        #region Properties

        private IHostingEnvironment HostingEnvironment { get; }

        #region IHostingEnvironment Properties

        //
        // Summary:
        //     Gets or sets the name of the environment. This property is automatically set
        //     by the host to the value of the "ASPNETCORE_ENVIRONMENT" environment variable.
        public string EnvironmentName { get { return HostingEnvironment.EnvironmentName; } set { HostingEnvironment.EnvironmentName = value; } }

        //
        // Summary:
        //     Gets or sets the name of the application. This property is automatically set
        //     by the host to the assembly containing the application entry point.
        public string ApplicationName { get { return HostingEnvironment.ApplicationName; } set { HostingEnvironment.ApplicationName = value; } }

        //
        // Summary:
        //     Gets or sets the absolute path to the directory that contains the web-servable
        //     application content files.
        public string WebRootPath { get { return HostingEnvironment.WebRootPath; } set { HostingEnvironment.WebRootPath = value; } }

        //
        // Summary:
        //     Gets or sets an Microsoft.Extensions.FileProviders.IFileProvider pointing at
        //     Microsoft.AspNetCore.Hosting.IHostingEnvironment.WebRootPath.
        public IFileProvider WebRootFileProvider { get { return HostingEnvironment.WebRootFileProvider; } set { HostingEnvironment.WebRootFileProvider = value; } }

        //
        // Summary:
        //     Gets or sets the absolute path to the directory that contains the application
        //     content files.
        public string ContentRootPath { get { return HostingEnvironment.ContentRootPath; } set { HostingEnvironment.ContentRootPath = value; } }

        //
        // Summary:
        //     Gets or sets an Microsoft.Extensions.FileProviders.IFileProvider pointing at
        //     Microsoft.AspNetCore.Hosting.IHostingEnvironment.ContentRootPath.
        public IFileProvider ContentRootFileProvider { get { return HostingEnvironment.ContentRootFileProvider; } set { HostingEnvironment.ContentRootFileProvider = value; } }

        #endregion

        #endregion

        #region Public Methods

        #region IHostingEnvironment Public Methods

        #endregion

        #endregion

        #region Private Methods

        private void InitializeHostingEnvironment()
        {
        }

        #endregion
    }
}
