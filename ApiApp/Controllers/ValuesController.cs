
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ApiApp.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ApiApp.Controllers.V_1_0
{
    [Produces("application/json")]
    [ApiVersion(CV.ApiVersions.V_1_0)]
    [Route("api/v{version:apiVersion}")]
    public class ValuesController : ControllerBase
    {
        #region Class Variables

        private static int _hits; //Just for fun...NOT thread-safe!!

        #endregion

        #region Constructors

        public ValuesController(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
            _hits += 1;
        }

        #endregion

        #region Properties
        #endregion

        #region Public Methods

        [HttpGet("[action]")]
        public IActionResult Ping()
        {
            return Ok($"{_hits} - {AppInfo.ApplicationName} - {AppInfo.EnvironmentName} - {DateTime.Now}");
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult AuthTest()
        {
            var userIdClaim = SiteUser.Claims.FirstOrDefault(); 

            return Ok($"{_hits} - {userIdClaim?.Type} - {userIdClaim?.Value} - {AppInfo.EnvironmentName} - {DateTime.Now}");
        }

        [HttpGet("[action]")]
        public IActionResult BadRequestTest()
        {
            return BadRequest($"{_hits} - {AppInfo.ApplicationName} - {AppInfo.EnvironmentName} - Bad Reuqest Test - {DateTime.Now}");
        }

        #endregion

        #region Private Methods
        #endregion
    }
}