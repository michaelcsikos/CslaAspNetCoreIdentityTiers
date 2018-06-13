using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CslaAspNetCoreIdentityTiers.DalEf
{
    public class DesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<EfDbContext>
    {
        const string CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Database=db;Trusted_Connection=True;"
                                       + "MultipleActiveResultSets=true";

        public EfDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfDbContext>();

            optionsBuilder.UseSqlServer(CONNECTION_STRING);

            return new EfDbContext(optionsBuilder.Options);
        }
    }
}
