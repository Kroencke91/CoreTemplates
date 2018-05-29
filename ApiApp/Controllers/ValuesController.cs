﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ApiApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ApiApp.Pipeline;
using System.Net;
using System.ComponentModel.DataAnnotations;
using ApiApp.Models;

namespace ApiApp.Controllers.V_1_0
{
    public class ValuesController : ControllerBase
    {
        #region Class Variables

        private static int _hits; //Just for fun...NOT thread-safe!!

        #endregion

        #region Constructors

        public ValuesController(IHttpContextAccessor contextAccessor, IValuesContext valuesContext)
            : base(contextAccessor, valuesContext)
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

        [HttpGet("[action]")]
        public IActionResult InternalServerTest()
        {
            var ex = new ApplicationException("Test Internal Server Exception");

            ex.Data.Add("Some Data", "Extra Error Details");

            ex.Data.Add("More Data", "Even More Extra Error Details");

            throw ex;
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult AuthenticationTest()
        {
            var userIdClaim = SiteUser.Claims.FirstOrDefault();

            return Ok($"{_hits} - {userIdClaim?.Type} - {userIdClaim?.Value} - {AppInfo.EnvironmentName} - {DateTime.Now}");
        }

        [Authorize(Roles = "SomeNonExistingRole")]
        [HttpGet("[action]")]
        public IActionResult AuthorizationTest()
        {
            var userIdClaim = SiteUser.Claims.FirstOrDefault();

            return Ok($"{_hits} - {userIdClaim?.Type} - {userIdClaim?.Value} - {AppInfo.EnvironmentName} - {DateTime.Now}");
        }

        [HttpGet("[action]")]
        public IActionResult ForbiddenTest()
        {
            return Forbid();
        }

        [HttpGet("[action]")]
        public IActionResult ValidationTest()
        {
            var vr = new ValidationResult("Some Bad Value was passed");

            var ex =  new ValidationException(vr, null, "");
                       
            throw ex;

            throw new ValidationException("Validation Failed");
        }

        [HttpPost("[action]")]
        public IActionResult PostValidationTest([FromBody] SampleModel model)
        {
            return Ok($"{_hits} - {AppInfo.ApplicationName} - {AppInfo.EnvironmentName} - PostValidationTest - {DateTime.Now}");
        }

        [HttpGet("[action]")]
        public IActionResult BadRequestTest1()
        {
            var badRequestError = new BadRequestError($"{_hits} - {AppInfo.ApplicationName} - {AppInfo.EnvironmentName} - Bad Reuqest Test 1 - Message Only - {DateTime.Now}");

            return BadRequest(badRequestError);
        }

        [HttpGet("[action]")]
        public IActionResult BadRequestTest2()
        {
            var innermostTestEx = new ApplicationException("Innermost Test Exception");

            var innertestEx = new ApplicationException("Inner Test Exception", innermostTestEx);

            var testEx = new ApplicationException("Test Exception", innertestEx);

            var badRequestError = new BadRequestError($"{_hits} - {AppInfo.ApplicationName} - {AppInfo.EnvironmentName} - Bad Reuqest Test 2 - Message & Exception - {DateTime.Now}", testEx);

            return BadRequest(badRequestError);
        }

        [HttpGet("[action]")]
        public IActionResult BadRequestTest3()
        {
            var innermostTestEx = new ApplicationException("Innermost Test Exception");

            var innertestEx = new ApplicationException("Inner Test Exception", innermostTestEx);

            var testEx = new ApplicationException("Test Exception - Exception Only", innertestEx);

            var badRequestError = new BadRequestError(testEx);

            return BadRequest(badRequestError);
        }

        #endregion

        #region Private Methods
        #endregion
    }
}