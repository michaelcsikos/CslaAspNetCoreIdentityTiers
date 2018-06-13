using System;
using System.Collections.Generic;
using System.Text;

namespace CslaAspNetCoreIdentityTiers.Dal
{
    public interface IDalManager
        : IDisposable
    {
        T GetProvider<T>() where T : class;
    }
}
