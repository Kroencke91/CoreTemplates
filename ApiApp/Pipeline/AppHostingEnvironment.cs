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
            InitializeFromHostingEnvironment(hostingEnvironment);

            InitializeAppHostingEnvironment();
        }

        #endregion

        #region Properties

        #region IHostingEnvironment Properties

        //
        // Summary:
        //     Gets or sets the name of the environment. This property is automatically set
        //     by the host to the value of the "ASPNETCORE_ENVIRONMENT" environment variable.
        public string EnvironmentName { get; private set; }

        //
        // Summary:
        //     Gets or sets the name of the application. This property is automatically set
        //     by the host to the assembly containing the application entry point.
        public string ApplicationName { get; private set; }

        //
        // Summary:
        //     Gets or sets the absolute path to the directory that contains the web-servable
        //     application content files.
        public string WebRootPath { get; private set; }

        //
        // Summary:
        //     Gets or sets an Microsoft.Extensions.FileProviders.IFileProvider pointing at
        //     Microsoft.AspNetCore.Hosting.IHostingEnvironment.WebRootPath.
        public  IFileProvider WebRootFileProvider { get; private set; }

        //
        // Summary:
        //     Gets or sets the absolute path to the directory that contains the application
        //     content files.
        public string ContentRootPath { get; private set; }

        //
        // Summary:
        //     Gets or sets an Microsoft.Extensions.FileProviders.IFileProvider pointing at
        //     Microsoft.AspNetCore.Hosting.IHostingEnvironment.ContentRootPath.
        public IFileProvider ContentRootFileProvider { get; private set; }

        #endregion

        #endregion

        #region Public Methods

        #region IHostingEnvironment Public Methods

        #endregion

        #endregion

        #region Private Methods

        private void InitializeFromHostingEnvironment(IHostingEnvironment hostingEnvironment)
        {
            EnvironmentName = hostingEnvironment.EnvironmentName;

            ApplicationName = hostingEnvironment.ApplicationName;

            WebRootPath = hostingEnvironment.WebRootPath;

            WebRootFileProvider = hostingEnvironment.WebRootFileProvider;

            ContentRootPath = hostingEnvironment.ContentRootPath;

            ContentRootFileProvider = hostingEnvironment.ContentRootFileProvider;
        }

        private void InitializeAppHostingEnvironment()
        {

        }

        #endregion
    }
}
