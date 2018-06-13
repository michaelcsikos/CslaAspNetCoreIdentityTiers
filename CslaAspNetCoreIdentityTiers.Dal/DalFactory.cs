using System;
using System.Collections.Generic;
using System.Text;

namespace CslaAspNetCoreIdentityTiers.Dal
{
    public static class DalFactory
    {
        static Type s_dalType;

        public static IDalManager GetManager()
        {
            if (s_dalType == null)
            {
                //var dalTypeName = ConfigurationManager.AppSettings["DalManagerType"];
                var dalTypeName = "CslaAspNetCoreIdentityTiers.DalEf.DalManager,CslaAspNetCoreIdentityTiers.DalEf";

                if (!string.IsNullOrEmpty(dalTypeName))
                    s_dalType = Type.GetType(dalTypeName);
                else
                    throw new NullReferenceException("DalManagerType");

                if (s_dalType == null)
                    throw new ArgumentException(string.Format("Type {0} could not be found", dalTypeName));
            }

            return (IDalManager)Activator.CreateInstance(s_dalType);
        }
    }
}
