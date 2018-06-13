using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CslaAspNetCoreIdentityTiers.DalEf
{
    [Table("Role")]
    public partial class Role
        : IdBase
    {
        [Required]
        [StringLength(256)]
        public string Name             { get; set; }

        [StringLength(256)]
        public string NormalizedName   { get; set; }

        [Timestamp]
        public byte[] ConcurrencyStamp { get; set; }
    }
}
