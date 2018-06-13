using System;
using System.Collections.Generic;
using System.Text;

namespace CslaAspNetCoreIdentityTiers.Dal
{
    public interface IAppUserDal
    {
        AppUserDto Fetch(Guid id);
        AppUserDto Fetch(string normalizedUserName);

        void       Insert(AppUserDto dto);
        void       Update(AppUserDto dto);
        void       Delete(Guid id);

        bool       ExistsId                (Guid   id);
        bool       ExistsNormalizedUserName(string normalizedUserName);
        Guid       GetIdByNormalizedEmail  (string normalizedEmail);
    }
}
