using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Interfaces
{
    interface IBVSafeSitesQuery
    {
        #region Properties

        string Source { get; set; }

        DateTime ModifiedAfter { get; set; }

        #endregion

        #region Public Methods
        #endregion
    }
}
