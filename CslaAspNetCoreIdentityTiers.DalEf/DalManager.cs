using System;
using System.Collections.Generic;
using System.Text;
using Csla.Data.EntityFrameworkCore;
using CslaAspNetCoreIdentityTiers.Dal;

namespace CslaAspNetCoreIdentityTiers.DalEf
{
    public class DalManager
        : IDalManager
    {
        static string s_typeMask = typeof(DalManager).FullName.Replace("DalManager", @"{0}");

        public T GetProvider<T>()
            where T : class
        {
            //var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            var typeName = string.Format(s_typeMask, typeof(T).Name.Substring(1));
            var type     = Type.GetType(typeName);

            if (type != null)
                return Activator.CreateInstance(type) as T;
            else
                throw new NotImplementedException(typeName);
        }

        public DbContextManager<EfDbContext> ConnectionManager { get; private set; }

        public DalManager()
        {
            ConnectionManager = DbContextManager<EfDbContext>.GetManager();
        }

        public void Dispose()
        {
            ConnectionManager.Dispose();

            ConnectionManager = null;
        }
    }
}
