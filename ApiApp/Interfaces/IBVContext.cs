using ApiApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Interfaces
{
    public interface IBVContext
    {
        #region Properties

        DbSet<BVSafeSite> BVSafeSites { get; set; }

        #endregion

        #region Public Methods
        #endregion
    }
}
