using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.DataAccess
{
    public class ValuesContext : DbContext
    {
        #region Class Variables
        #endregion

        #region Constructors

        public ValuesContext(DbContextOptions<ValuesContext> options) : base(options)
        {
        }

        #endregion

        #region Properties
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
