using System;
using System.Collections.Generic;
using System.Text;

namespace CslaAspNetCoreIdentityTiers.Dal
{
    public class AppUserDto
    {
        public Guid            Id                     { get; set; }
        public string          UserName               { get; set; }
        public string          NormalizedUserName     { get; set; }
        public string          Email                  { get; set; }
        public string          NormalizedEmail        { get; set; }
        public bool            EmailConfirmed         { get; set; }
        public string          PasswordHash           { get; set; }
        public string          SecurityStamp          { get; set; }
        public string          PhoneNumber            { get; set; }
        public bool            PhoneNumberConfirmed   { get; set; }
        public bool            TwoFactorEnabled       { get; set; }
        public DateTimeOffset? LockoutEnd             { get; set; }
        public bool            LockoutEnabled         { get; set; }
        public int             AccessFailedCount      { get; set; }
        public byte[]          ConcurrencyStamp       { get; set; }
    }
}
