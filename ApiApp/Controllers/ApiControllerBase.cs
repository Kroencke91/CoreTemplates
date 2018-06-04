
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

using ApiApp.Interfaces;
using ApiApp.Misc;
using Microsoft.Extensions.Caching.Memory;
using ApiApp.Pipeline;

namespace ApiApp.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}")]
    [ValidateModel]
    public abstract class ApiControllerBase : Controller
    {
        #region Class Variables
        
        private static IAppInfo _appInfo;

        #endregion

        #region Constructors

        protected ApiControllerBase(IHttpContextAccessor contextAccessor, IValueRepository valueRepository)
        {
            ContextAccessor = contextAccessor;

            ValueRepository = valueRepository;

            RequestedApiVersion = ContextAccessor.HttpContext.GetRequestedApiVersion().ToString();

            SiteUser = new SiteUser(ContextAccessor.HttpContext.User);
        }

        #endregion

        #region Properties

        protected ILogger Log => _appInfo.Env.Log;

        protected IHttpContextAccessor ContextAccessor { get; private set; }

        protected IValueRepository ValueRepository { get; private set; }

        protected IAppInfo AppInfo => _appInfo;

        protected ISiteUser SiteUser { get; private set; }

        protected IMemoryCache MemoryCache => _appInfo.MemoryCache;

        protected string RequestedApiVersion { get; }

        #endregion

        #region Public Methods

        //internal static void InitMemoryCache(IMemoryCache memoryCache)
        //{
        //    if (_memoryCache != null) throw new ApplicationException("MemoryCache already intialized!");

        //    _memoryCache = memoryCache;
        //}

        internal static void InitAppInfo(IAppInfo appInfo)
        {
            if (_appInfo != null) throw new ApplicationException("AppInfo already intialized!");

            _appInfo = appInfo;
        }

        public Pipeline.UnauthorizedResult Unauthorized(object error)
        {
            return new Pipeline.UnauthorizedResult(error);
        }

        public override Microsoft.AspNetCore.Mvc.UnauthorizedResult Unauthorized()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Protected Methods

        protected string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);

            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        protected string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            return Convert.ToBase64String(plainTextBytes);
        }

        #endregion

        #region Private Methods
        #endregion
    }
}