using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Models
{
    public class Site
    {
        public Site()
        {
            Projects = new List<Project>();
        }

        public int SiteIndex { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Active { get; set; }

        public string SiteName { get; set; }

        public List<Project> Projects { get; }
    }
}
