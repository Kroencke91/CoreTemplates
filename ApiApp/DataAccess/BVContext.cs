using ApiApp.Interfaces;
using ApiApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.DataAccess
{
    public class BVContext : DbContext, IBVContext
    {
        #region Class Variables
        #endregion

        #region Constructors

        public BVContext(DbContextOptions<BVContext> options)
            : base(options)
        {
        }

        #endregion

        #region Properties

        public DbSet<BVSafeSite> BVSafeSites { get; set; }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BVSafeSite>()
                .HasKey(o => new { o.Latitude, o.Longitude, o.ProjectId});
        }

        #endregion
    }
}
