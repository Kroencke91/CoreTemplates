using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Models
{
    public class SitesInfo
    {
        public SitesInfo()
        {
            Sites = new List<Site>();

            Errors = new List<Error>();
        }

        public int SiteCount { get; set; }

        public List<Site> Sites { get; }

        public List<Error> Errors { get; }
    }
}
