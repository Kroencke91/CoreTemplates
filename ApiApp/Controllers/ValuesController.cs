
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
using Serilog;

namespace ApiApp.Controllers.V_1_0
{
    [ApiVersion(CV.ApiVersions.V_1_0)]
    public class ValuesController : ApiControllerBase
    {
        #region Class Variables

        private static int _hits; //Just for fun...NOT thread-safe!!

        private IBVSafeSiteRepository _bvSafeSiteRepository;

        #endregion

        #region Constructors

        public ValuesController(IHttpContextAccessor contextAccessor, IValueRepository valueRepository, IBVSafeSiteRepository bvSafeSiteRepository)
            : base(contextAccessor, valueRepository)
        {
            _hits += 1;

            _bvSafeSiteRepository = bvSafeSiteRepository;
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
        public IActionResult BVSafeSites()
        {
            var sitesInfo = new SitesInfo();

            try
            {
                //throw new ApplicationException("TEST BVSafeSites ERROR HANDLING");

                var sites = _bvSafeSiteRepository.BVSafeSites.Take(6);

                var currSiteLat = "";

                var currSiteLong = "";

                var curSite = new Site();

                var siteCount = 0;

                var projectCount = 0;

                foreach (var site in sites)
                {
                    if (!(site.Latitude == currSiteLat && site.Longitude == currSiteLong))
                    {
                        siteCount += 1;

                        projectCount = 0;

                        try
                        {
                            if (siteCount == 2) throw new ApplicationException("TEST BVSafeSites: Add Site ERROR HANDLING");

                            curSite = new Site()
                            {
                                SiteIndex = siteCount,
                                Latitude = site.Latitude,
                                Longitude = site.Longitude,
                                Active = site.Active,
                                SiteName = site.SiteName,
                            };

                            sitesInfo.Sites.Add(curSite);
                        }
                        catch (Exception ex)
                        {
                            //TODO: Add Logging

                            var error = new Error();

                            error.Message = $"Add Site {siteCount}: {ex.Message}";

                            sitesInfo.Errors.Add(error);

                            curSite = new Site();

                            continue;
                        }
                    }

                    projectCount += 1;

                    try
                    {
                        if (siteCount == 4) throw new ApplicationException("TEST BVSafeSites: Add Project ERROR HANDLING");

                        var proj = new Project()
                        {
                            ProjectId = site.ProjectId,
                            ProjectName = site.ProjectName,
                            Active = site.Active,
                        };

                        curSite.Projects.Add(proj);
                    }
                    catch (Exception ex)
                    {
                        //TODO: Add Logging

                        var error = new Error();

                        error.Message = $"Add Site {siteCount} Project {projectCount}: {ex.Message}";

                        sitesInfo.Errors.Add(error);
                    }
                }

                sitesInfo.SiteCount = siteCount;
            }
            catch (Exception ex)
            {
                //TODO: Add Logging

                var error = new Error();

                error.Message = $"{ex.Message}";

                sitesInfo.Errors.Add(error);
            }

            return Ok(sitesInfo);
        }

        //[HttpPost("[action]")]
        //public IActionResult BVSafeSites()
        //{
        //    var sitesInfo = new SitesInfo();

        //    try
        //    {
        //        //throw new ApplicationException("TEST BVSafeSites ERROR HANDLING");

        //        var sites = _bvSafeSiteRepository.BVSafeSites.Take(6);

        //        var currSiteLat = "";

        //        var currSiteLong = "";

        //        var curSite = new Site();

        //        var siteCount = 0;

        //        var projectCount = 0;

        //        foreach (var site in sites)
        //        {
        //            if (!(site.Latitude == currSiteLat && site.Longitude == currSiteLong))
        //            {
        //                siteCount += 1;

        //                projectCount = 0;

        //                try
        //                {
        //                    if (siteCount == 2) throw new ApplicationException("TEST BVSafeSites: Add Site ERROR HANDLING");

        //                    curSite = new Site()
        //                    {
        //                        SiteIndex = siteCount,
        //                        Latitude = site.Latitude,
        //                        Longitude = site.Longitude,
        //                        Active = site.Active,
        //                        SiteName = site.SiteName,
        //                    };

        //                    sitesInfo.Sites.Add(curSite);
        //                }
        //                catch (Exception ex)
        //                {
        //                    //TODO: Add Logging

        //                    var error = new Error();

        //                    error.Message = $"Add Site {siteCount}: {ex.Message}";

        //                    sitesInfo.Errors.Add(error);

        //                    curSite = new Site();

        //                    continue;
        //                }
        //            }

        //            projectCount += 1;

        //            try
        //            {
        //                if (siteCount == 4) throw new ApplicationException("TEST BVSafeSites: Add Project ERROR HANDLING");

        //                var proj = new Project()
        //                {
        //                    ProjectId = site.ProjectId,
        //                    ProjectName = site.ProjectName,
        //                    Active = site.Active,
        //                };

        //                curSite.Projects.Add(proj);
        //            }
        //            catch (Exception ex)
        //            {
        //                //TODO: Add Logging

        //                var error = new Error();

        //                error.Message = $"Add Site {siteCount} Project {projectCount}: {ex.Message}";

        //                sitesInfo.Errors.Add(error);
        //            }
        //        }

        //        sitesInfo.SiteCount = siteCount;
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO: Add Logging

        //        var error = new Error();

        //        error.Message = $"{ex.Message}";

        //        sitesInfo.Errors.Add(error);
        //    }

        //    return Ok(sitesInfo);
        //}

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

        [Authorize()]
        [HttpGet("[action]")]
        public IActionResult AuthorizationTest()
        {
            var userIdClaim = SiteUser.Claims.FirstOrDefault();

            return Ok($"{_hits} - {userIdClaim?.Type} - {userIdClaim?.Value} - {AppInfo.EnvironmentName} - {DateTime.Now}");
        }

        [HttpGet("[action]")]
        public IActionResult ForbiddenTest()
        {
            Log.Debug($"Forbidden - {DateTime.Now}");

            return Forbid();
        }

        [HttpGet("[action]")]
        public IActionResult ValidationTest()
        {
            var vr = new ValidationResult("Some Bad Value was passed");

            var ex = new ValidationException(vr, null, "");

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