
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ApiApp.Interfaces;

namespace ApiApp.Controllers.V_1_0
{
    [Produces("application/json")]
    [ApiVersion(CV.ApiVersions.V_1_0)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ValuesController : ControllerBase
    {
        #region Class Variables
        #endregion

        #region Constructors

        public ValuesController(IAppInfo appInfo)
            : base(appInfo)
        {

        }

        #endregion

        #region Properties
        #endregion

        #region Public Methods

        [HttpGet]
        public IActionResult Ping()
        {
            return Ok($"{AppInfo.ApplicationName} - {AppInfo.EnvironmentName} - {DateTime.Now}");
        }

        #endregion

        #region Private Methods
        #endregion
    }
}