using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Interfaces
{
    public interface IValuesContext
    {
        #region Properties
        #endregion

        #region Public Methods

        IEnumerable<int> List { get; }

        #endregion
    }
}
