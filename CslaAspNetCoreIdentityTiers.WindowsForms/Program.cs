using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CslaAspNetCoreIdentityTiers.WindowsForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Csla.DataPortal.ProxyTypeName              = typeof(Csla.DataPortalClient.HttpProxy).AssemblyQualifiedName;
            Csla.DataPortalClient.HttpProxy.DefaultUrl = "http://localhost:51086/api/DataPortal/PostAsync";
            Csla.ApplicationContext.User               = new Csla.Security.UnauthenticatedPrincipal();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
