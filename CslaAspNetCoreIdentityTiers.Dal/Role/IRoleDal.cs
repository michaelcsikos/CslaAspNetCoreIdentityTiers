using System;
using System.Collections.Generic;
using System.Text;

namespace CslaAspNetCoreIdentityTiers.Dal
{
    public interface IRoleDal
    {
        List<RoleDto> Fetch();
        RoleDto       Fetch(Guid   id);
        RoleDto       Fetch(string normalizedName);

        void          Insert(RoleDto dto);
        void          Update(RoleDto dto);
        void          Delete(Guid id);

        bool          ExistsId  (Guid   id);
        bool          ExistsName(string normalizedName);
    }
}
