using ApiApp.DataAccess;
using ApiApp.Interfaces;
using ApiApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Repositories
{
    public class BVSafeSiteRepository : IBVSafeSiteRepository
    {
        #region Class Variables

        private BVContext _context;

        #endregion

        #region Constructors

        public BVSafeSiteRepository(BVContext context)
        {
            _context = context;
        }

        #endregion

        #region Properties

        public IQueryable<BVSafeSite> BVSafeSites => _context.BVSafeSites.FromSql("exec BVSAFE.util.GetBVSafeSites"); 

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
