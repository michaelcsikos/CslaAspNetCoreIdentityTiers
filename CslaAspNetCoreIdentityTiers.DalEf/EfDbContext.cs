using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CslaAspNetCoreIdentityTiers.DalEf
{
    public partial class EfDbContext
        : DbContext
    {
        public const string CONNECTION_STRING_NAME = "DefaultConnection";

        public EfDbContext()
        {
        }

        public EfDbContext(DbContextOptions<EfDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var c = "Server=(localdb)\\mssqllocaldb;Database=CslaAspNetCoreIdentityTiers-sqldb;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(c);
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Role>    Roles    { get; set; }

        #region Extensions

        public EntityEntry<TEntity> AttachAsModified<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entry   = Entry(entity);

            entry.State = EntityState.Modified;

            return entry;
        }

        #endregion
    }
}
