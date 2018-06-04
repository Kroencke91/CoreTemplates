using ApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Interfaces
{
    public interface IBVSafeSiteRepository
    {
        #region Properties

        IQueryable<BVSafeSite> BVSafeSites { get; }

        #endregion

        #region Public Methods
        #endregion
    }
}
