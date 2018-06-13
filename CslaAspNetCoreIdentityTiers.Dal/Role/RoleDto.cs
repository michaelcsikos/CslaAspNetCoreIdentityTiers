using System;
using System.Collections.Generic;
using System.Text;

namespace CslaAspNetCoreIdentityTiers.Dal
{
    public class RoleDto
    {
        public Guid   Id               { get; set; }
        public string Name             { get; set; }
        public string NormalizedName   { get; set; }
        public byte[] ConcurrencyStamp { get; set; }
    }
}
