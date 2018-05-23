
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ApiApp.Interfaces;

namespace ApiApp.Controllers
{
    [Produces("application/json")]
    public abstract class ControllerBase : Controller
    {
        #region Class Variables
        #endregion

        #region Constructors

        protected ControllerBase(IAppInfo appInfo)
        {
            AppInfo = appInfo;
        }

        #endregion

        #region Properties

        protected IAppInfo AppInfo { get; }

        #endregion

        #region Public Methods
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