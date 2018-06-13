using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Csla;
using CslaAspNetCoreIdentityTiers.Dal;

namespace CslaAspNetCoreIdentityTiers.Business
{
    [Serializable]
    public class AppUser
        : BusinessBase<AppUser>
    {
        #region Business

        public static readonly PropertyInfo<Guid> IdProperty
                         = RegisterProperty<Guid>(p => p.Id);
        public Guid Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> UserNameProperty
                         = RegisterProperty<string>(p => p.UserName);
        [Required]
        [StringLength(256)]
        [Display(Name = "User name")]
        public string UserName
        {
            get { return GetProperty(UserNameProperty); }
            set { SetProperty(UserNameProperty, value); }
        }

        public string NormalizedUserName
        {
            get { return NormalizeUserName(UserName); }
        }

        public static string NormalizeUserName(string userName)
        {
            return userName.ToUpperInvariant();
        }

        public static readonly PropertyInfo<string> EmailProperty
                         = RegisterProperty<string>(p => p.Email);
        [Required]
        [EmailAddress]
        [StringLength(256)]
        [Display(Name = "E-mail")]
        public string Email
        {
            get { return GetProperty(EmailProperty); }
            set { SetProperty(EmailProperty, value); }
        }

        public string NormalizedEmail
        {
            get { return NormalizeEmail(Email); }
        }

        public static string NormalizeEmail(string email)
        {
            return email.ToUpperInvariant();
        }

        public static readonly PropertyInfo<bool> EmailConfirmedProperty
                         = RegisterProperty<bool>(p => p.EmailConfirmed);
        public bool EmailConfirmed
        {
            get { return GetProperty(EmailConfirmedProperty); }
            set { SetProperty(EmailConfirmedProperty, value); }
        }

        public static readonly PropertyInfo<string> PasswordHashProperty
                         = RegisterProperty<string>(p => p.PasswordHash);
        public string PasswordHash
        {
            get { return GetProperty(PasswordHashProperty); }
            set { SetProperty(PasswordHashProperty, value); }
        }

        public static readonly PropertyInfo<string> SecurityStampProperty
                         = RegisterProperty<string>(p => p.SecurityStamp);
        [Display(Name = "Security stamp")]
        public string SecurityStamp
        {
            get { return GetProperty(SecurityStampProperty); }
            set { SetProperty(SecurityStampProperty, value); }
        }

        public static readonly PropertyInfo<string> PhoneNumberProperty
                         = RegisterProperty<string>(p => p.PhoneNumber);
        [Display(Name = "Phone number")]
        public string PhoneNumber
        {
            get { return GetProperty(PhoneNumberProperty); }
            set { SetProperty(PhoneNumberProperty, value); }
        }

        public static readonly PropertyInfo<bool> PhoneNumberConfirmedProperty
                         = RegisterProperty<bool>(p => p.PhoneNumberConfirmed);
        public bool PhoneNumberConfirmed
        {
            get { return GetProperty(PhoneNumberConfirmedProperty); }
            set { SetProperty(PhoneNumberConfirmedProperty, value); }
        }

        public static readonly PropertyInfo<bool> TwoFactorEnabledProperty
                         = RegisterProperty<bool>(p => p.TwoFactorEnabled);
        [Display(Name = "Two-factor enabled")]
        public bool TwoFactorEnabled
        {
            get { return GetProperty(TwoFactorEnabledProperty); }
            set { SetProperty(TwoFactorEnabledProperty, value); }
        }

        public static readonly PropertyInfo<DateTimeOffset?> LockoutEndProperty
                         = RegisterProperty<DateTimeOffset?>(p => p.LockoutEnd);
        [Display(Name = "Lockout end")]
        public DateTimeOffset? LockoutEnd
        {
            get { return GetProperty(LockoutEndProperty); }
            set { SetProperty(LockoutEndProperty, value); }
        }

        public static readonly PropertyInfo<bool> LockoutEnabledProperty
                         = RegisterProperty<bool>(p => p.LockoutEnabled);
        [Display(Name = "Lockout enabled")]
        public bool LockoutEnabled
        {
            get { return GetProperty(LockoutEnabledProperty); }
            set { SetProperty(LockoutEnabledProperty, value); }
        }

        public static readonly PropertyInfo<int> AccessFailedCountProperty
                         = RegisterProperty<int>(p => p.AccessFailedCount);
        [Display(Name = "Access failed count")]
        public int AccessFailedCount
        {
            get { return GetProperty(AccessFailedCountProperty); }
            set { SetProperty(AccessFailedCountProperty, value); }
        }

        public static readonly PropertyInfo<byte[]> ConcurrencyStampProperty
                         = RegisterProperty<byte[]>(p => p.ConcurrencyStamp);
        public byte[] ConcurrencyStamp
        {
            get { return GetProperty(ConcurrencyStampProperty); }
            set { SetProperty(ConcurrencyStampProperty, value); }
        }

        #endregion

        #region Roles

        //public static readonly PropertyInfo<AppUserRoles> RolesProperty
        //                 = RegisterProperty<AppUserRoles>(p => p.Roles, StringsCcb.Roles, RelationshipTypes.LazyLoad);
        //public AppUserRoles Roles
        //{
        //    get { return LazyGetProperty(RolesProperty, DataPortal.CreateChild<AppUserRoles>); }
        //    private set { LoadProperty(RolesProperty, value); }
        //}

        #endregion

        #region Factory methods

        public static AppUser NewAppUser()
        {
            return DataPortal.Create<AppUser>();
        }

        public static AppUser GetAppUser(Guid id)
        {
            return DataPortal.Fetch<AppUser>(id);
        }

        public static async Task<AppUser> GetAppUserAsync(Guid id)
        {
            return await DataPortal.FetchAsync<AppUser>(id);
        }

        public static AppUser GetAppUser(string normalizedUserName)
        {
            return DataPortal.Fetch<AppUser>(normalizedUserName);
        }

        public static async Task<AppUser> GetAppUserAsync(string normalizedUserName)
        {
            return await DataPortal.FetchAsync<AppUser>(normalizedUserName);
        }

        public static void DeleteAppUser(Guid id)
        {
            DataPortal.Delete<AppUser>(id);
        }

        public static async Task DeleteAppUserAsync(Guid id)
        {
            await DataPortal.DeleteAsync<AppUser>(id);
        }

        #endregion

        #region Data access

        protected override void DataPortal_Create()
        {
            Id = Guid.NewGuid();

            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(Guid id)
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IAppUserDal>();
                var dto = dal.Fetch(id);

                FromDto(dto);

                //Roles = DataPortal.FetchChild<AppUserRoles>(Id);
            }
        }

        private void DataPortal_Fetch(string normalizedUserName)
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IAppUserDal>();
                var dto = dal.Fetch(normalizedUserName);

                FromDto(dto);

                //Roles = DataPortal.FetchChild<AppUserRoles>(Id);
            }
        }

        private void FromDto(AppUserDto dto)
        {
            using (BypassPropertyChecks)
            {
                Id                   = dto.Id;
                UserName             = dto.UserName;
                Email                = dto.Email;
                PasswordHash         = dto.PasswordHash;
                SecurityStamp        = dto.SecurityStamp;
                PhoneNumber          = dto.PhoneNumber;
                PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
                TwoFactorEnabled     = dto.TwoFactorEnabled;
                LockoutEnd           = dto.LockoutEnd;
                LockoutEnabled       = dto.LockoutEnabled;
                AccessFailedCount    = dto.AccessFailedCount;
                ConcurrencyStamp     = dto.ConcurrencyStamp;
            }
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IAppUserDal>();
                var dto = ToDto();

                dal.Insert(dto);

                using (BypassPropertyChecks)
                    ConcurrencyStamp = dto.ConcurrencyStamp;

                UpdateChildren();
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DalFactory.GetManager())
            {
                if (IsSelfDirty)
                {
                    var dal = ctx.GetProvider<IAppUserDal>();
                    var dto = ToDto();

                    dal.Update(dto);

                    using (BypassPropertyChecks)
                        ConcurrencyStamp = dto.ConcurrencyStamp;
                }

                UpdateChildren();
            }
        }

        private AppUserDto ToDto()
        {
            var dto = new AppUserDto();

            using (BypassPropertyChecks)
            {
                dto.Id                   = Id;
                dto.UserName             = UserName;
                dto.NormalizedUserName   = UserName.ToUpperInvariant();
                dto.Email                = Email;
                dto.NormalizedEmail      = Email   .ToUpperInvariant();
                dto.PasswordHash         = PasswordHash;
                dto.SecurityStamp        = SecurityStamp;
                dto.PhoneNumber          = PhoneNumber;
                dto.PhoneNumberConfirmed = PhoneNumberConfirmed;
                dto.TwoFactorEnabled     = TwoFactorEnabled;
                dto.LockoutEnd           = LockoutEnd;
                dto.LockoutEnabled       = LockoutEnabled;
                dto.AccessFailedCount    = AccessFailedCount;
                dto.ConcurrencyStamp     = ConcurrencyStamp;
            }

            return dto;
        }

        private void UpdateChildren()
        {
            //if (FieldManager.FieldExists(RolesProperty))
            //    DataPortal.UpdateChild(ReadProperty(RolesProperty), this);
        }

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(Id);
        }

        private void DataPortal_Delete(Guid id)
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IAppUserDal>();

                dal.Delete(id);
            }
        }

        #endregion

        #region Exists Id

        public static bool ExistsId(Guid id)
        {
            var cmd = new CommandAppUserExistsId(id);

            cmd     = DataPortal.Execute(cmd);

            return cmd.Exists;
        }

        public static async Task<bool> ExistsIdAsync(Guid id)
        {
            var cmd = new CommandAppUserExistsId(id);

            cmd     = await DataPortal.ExecuteAsync(cmd);

            return cmd.Exists;
        }

        [Serializable]
        private class CommandAppUserExistsId
            : CommandBase<CommandAppUserExistsId>
        {
            public static readonly PropertyInfo<Guid> IdProperty
                             = RegisterProperty<Guid>(p => p.Id);
            public Guid Id
            {
                get { return ReadProperty(IdProperty); }
                set { LoadProperty(IdProperty, value); }
            }

            public static readonly PropertyInfo<bool> ExistsProperty
                             = RegisterProperty<bool>(p => p.Exists);
            public bool Exists
            {
                get { return ReadProperty(ExistsProperty); }
                set { LoadProperty(ExistsProperty, value); }
            }

            public CommandAppUserExistsId()
            {
            }

            public CommandAppUserExistsId(Guid id)
            {
                Id = id;
            }

            protected override void DataPortal_Execute()
            {
                using (var ctx = DalFactory.GetManager())
                {
                    var dal = ctx.GetProvider<IAppUserDal>();

                    Exists  = dal.ExistsId(Id);
                }
            }
        }

        #endregion

        #region Exists user name

        public static bool ExistsNormalizedUserName(string normalizedUserName)
        {
            var cmd = new CommandAppUserExistsNormalizedUserName(normalizedUserName);

            cmd     = DataPortal.Execute(cmd);

            return cmd.Exists;
        }

        public static async Task<bool> ExistsNormalizedUserNameAsync(string normalizedUserName)
        {
            var cmd = new CommandAppUserExistsNormalizedUserName(normalizedUserName);

            cmd     = await DataPortal.ExecuteAsync(cmd);

            return cmd.Exists;
        }

        [Serializable]
        public class CommandAppUserExistsNormalizedUserName
            : CommandBase<CommandAppUserExistsNormalizedUserName>
        {
            public static readonly PropertyInfo<string> NormalizedUserNameProperty
                             = RegisterProperty<string>(p => p.NormalizedUserName);
            public string NormalizedUserName
            {
                get { return ReadProperty(NormalizedUserNameProperty); }
                private set { LoadProperty(NormalizedUserNameProperty, value); }
            }

            public static readonly PropertyInfo<bool> ExistsProperty
                             = RegisterProperty<bool>(p => p.Exists);
            public bool Exists
            {
                get { return ReadProperty(ExistsProperty); }
                private set { LoadProperty(ExistsProperty, value); }
            }

            public CommandAppUserExistsNormalizedUserName()
            {
            }

            public CommandAppUserExistsNormalizedUserName(string normalizedUserName)
            {
                NormalizedUserName = normalizedUserName;
            }

            protected override void DataPortal_Execute()
            {
                using (var ctx = DalFactory.GetManager())
                {
                    var dal = ctx.GetProvider<IAppUserDal>();

                    Exists  = dal.ExistsNormalizedUserName(NormalizedUserName);
                }
            }
        }

        #endregion

        #region Get Id by normalized e-mail

        public static Guid GetIdByNormalizedEmail(string normalizedEmail)
        {
            var cmd = new CommandAppUserGetIdByNormalizedEmail(normalizedEmail);

            cmd     = DataPortal.Execute(cmd);

            return cmd.Id;
        }

        public static async Task<Guid> GetIdByNormalizedEmailAsync(string normalizedEmail)
        {
            var cmd = new CommandAppUserGetIdByNormalizedEmail(normalizedEmail);

            cmd     = await DataPortal.ExecuteAsync(cmd);

            return cmd.Id;
        }

        [Serializable]
        private class CommandAppUserGetIdByNormalizedEmail
            : CommandBase<CommandAppUserGetIdByNormalizedEmail>
        {
            public static readonly PropertyInfo<string> NormalizedEmailProperty
                             = RegisterProperty<string>(p => p.NormalizedEmail);
            public string NormalizedEmail
            {
                get { return ReadProperty(NormalizedEmailProperty); }
                set { LoadProperty(NormalizedEmailProperty, value); }
            }

            public static readonly PropertyInfo<Guid> IdProperty
                             = RegisterProperty<Guid>(p => p.Id);
            public Guid Id
            {
                get { return ReadProperty(IdProperty); }
                set { LoadProperty(IdProperty, value); }
            }

            public CommandAppUserGetIdByNormalizedEmail()
            {
            }

            public CommandAppUserGetIdByNormalizedEmail(string normalizedEmail)
            {
                NormalizedEmail = normalizedEmail;
            }

            protected override void DataPortal_Execute()
            {
                using (var ctx = DalFactory.GetManager())
                {
                    var dal = ctx.GetProvider<IAppUserDal>();

                    Id      = dal.GetIdByNormalizedEmail(NormalizedEmail);
                }
            }
        }

        #endregion
    }
}
