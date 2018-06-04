using ApiApp.Interfaces;
using ApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Repositories
{
    public class ValueRepository : IValueRepository
    {
        #region Class Variables
        #endregion

        #region Constructors
        #endregion

        #region Properties

       public IQueryable<BVSafeSite> BVSafeSites { get; }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
