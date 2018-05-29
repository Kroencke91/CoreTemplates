
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ApiApp.Interfaces;
using ApiApp.Misc;
using Microsoft.Extensions.Caching.Memory;
using ApiApp.Pipeline;

namespace ApiApp.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}")]
    //[ValidateAntiForgeryToken]
    [ValidateModel]
    public abstract class ControllerBase : Controller
    {
        #region Class Variables

        private static IAppInfo _appInfo;

        private static IMemoryCache _memoryCache;
        
        private readonly string _requestedApiVersion;

        #endregion

        #region Constructors

        protected ControllerBase(IHttpContextAccessor contextAccessor, IValuesContext valuesContext)
        {
            ContextAccessor = contextAccessor;

            ValuesContext = valuesContext;

            _requestedApiVersion = ContextAccessor.HttpContext.GetRequestedApiVersion().ToString();

            SiteUser = new SiteUser(ContextAccessor.HttpContext.User);
        }

        #endregion

        #region Properties

        protected IHttpContextAccessor ContextAccessor { get; private set; }

        protected IValuesContext ValuesContext { get; private set; }

        protected string RequestedApiVersion => _requestedApiVersion;

        protected IAppInfo AppInfo => _appInfo;

        protected ISiteUser SiteUser { get; private set; }

        protected IMemoryCache MemoryCache => _memoryCache;

        #endregion

        #region Public Methods

        internal static void InitMemoryCache(IMemoryCache memoryCache)
        {
            if (_memoryCache != null) throw new ApplicationException("MemoryCache already intialized!");

            _memoryCache = memoryCache;
        }

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