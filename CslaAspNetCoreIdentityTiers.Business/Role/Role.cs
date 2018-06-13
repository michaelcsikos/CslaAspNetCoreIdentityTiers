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
    public class Role
        : BusinessBase<Role>
    {
        #region Constants

        public static readonly Guid   ADMINISTRATOR_ID = new Guid("00000000-0000-0000-0000-000000000001"),
                                      USER_ID          = new Guid("00000000-0000-0000-0000-000000000002");

        public const           string ADMINISTRATOR    = "Administrator",
                                      USER             = "User";

        #endregion

        #region Business

        public static readonly PropertyInfo<Guid> IdProperty
                         = RegisterProperty<Guid>(p => p.Id);
        public Guid Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty
                         = RegisterProperty<string>(p => p.Name);
        [Required]
        [StringLength(256)]
        [Display(Name = "Name")]
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public string NormalizedName
        {
            get { return NormalizeName(Name); }
        }

        public static string NormalizeName(string name)
        {
            return name.ToUpperInvariant();
        }

        public static readonly PropertyInfo<byte[]> ConcurrencyStampProperty
                         = RegisterProperty<byte[]>(p => p.ConcurrencyStamp);
        public byte[] ConcurrencyStamp
        {
            get { return GetProperty(ConcurrencyStampProperty); }
            set { SetProperty(ConcurrencyStampProperty, value); }
        }

        #endregion

        #region Get constant role name

        public static string GetRoleName(Guid id)
        {
            if (id == ADMINISTRATOR_ID)
                return ADMINISTRATOR;

            if (id == USER_ID)
                return USER;

            return string.Empty;
        }

        #endregion

        #region Factory methods

        public static Role NewRole()
        {
            return DataPortal.Create<Role>();
        }

        public static Role GetRole(Guid id)
        {
            return DataPortal.Fetch<Role>(id);
        }

        public static async Task<Role> GetRoleAsync(Guid id)
        {
            return await DataPortal.FetchAsync<Role>(id);
        }

        public static Role GetRole(string name)
        {
            return DataPortal.Fetch<Role>(name);
        }

        public static async Task<Role> GetRoleAsync(string normalizedName)
        {
            return await DataPortal.FetchAsync<Role>(normalizedName);
        }

        public static void DeleteRole(Guid id)
        {
            DataPortal.Delete<Role>(id);
        }

        public static async Task DeleteRoleAsync(Guid id)
        {
            await DataPortal.DeleteAsync<Role>(id);
        }

        #endregion

        #region Data access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            Id = Guid.NewGuid();

            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(Guid id)
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IRoleDal>();
                var dto = dal.Fetch(id);

                FromDto(dto);
            }
        }

        private void DataPortal_Fetch(string normalizedName)
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IRoleDal>();
                var dto = dal.Fetch(normalizedName);

                FromDto(dto);
            }
        }

        private void FromDto(RoleDto dto)
        {
            using (BypassPropertyChecks)
            {
                Id               = dto.Id;
                Name             = dto.Name;
                ConcurrencyStamp = dto.ConcurrencyStamp;
            }
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IRoleDal>();
                var dto = ToDto();

                dal.Insert(dto);

                using (BypassPropertyChecks)
                    ConcurrencyStamp = dto.ConcurrencyStamp;
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IRoleDal>();
                var dto = ToDto();

                dal.Update(dto);

                using (BypassPropertyChecks)
                    ConcurrencyStamp = dto.ConcurrencyStamp;
            }
        }

        private RoleDto ToDto()
        {
            var dto  = new RoleDto();

            using (BypassPropertyChecks)
            {
                dto.Id               = Id;
                dto.Name             = Name;
                dto.NormalizedName   = NormalizedName;
                dto.ConcurrencyStamp = ConcurrencyStamp;
            }

            return dto;
        }

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(Id);
        }

        private void DataPortal_Delete(Guid id)
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IRoleDal>();

                dal.Delete(id);
            }
        }

        #endregion

        #region Exists Id

        public static bool ExistsId(Guid id)
        {
            var cmd = new CommandRoleExistsId(id);

            cmd     = DataPortal.Execute(cmd);

            return cmd.Exists;
        }

        public static async Task<bool> ExistsIdAsync(Guid id)
        {
            var cmd = new CommandRoleExistsId(id);

            cmd     = await DataPortal.ExecuteAsync(cmd);

            return cmd.Exists;
        }

        [Serializable]
        private class CommandRoleExistsId
            : CommandBase<CommandRoleExistsId>
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
                private set { LoadProperty(ExistsProperty, value); }
            }

            public CommandRoleExistsId()
            {
            }

            public CommandRoleExistsId(Guid id)
            {
                Id = id;
            }

            protected override void DataPortal_Execute()
            {
                using (var ctx = DalFactory.GetManager())
                {
                    var dal = ctx.GetProvider<IRoleDal>();

                    Exists  = dal.ExistsId(Id);
                }
            }
        }

        #endregion

        #region Exists name

        public static bool ExistsName(string name)
        {
            var cmd = new CommandRoleExistsName(name);

            cmd     = DataPortal.Execute(cmd);

            return cmd.Exists;
        }

        public static async Task<bool> ExistsNameAsync(string name)
        {
            var cmd = new CommandRoleExistsName(name);

            cmd     = await DataPortal.ExecuteAsync(cmd);

            return cmd.Exists;
        }

        [Serializable]
        private class CommandRoleExistsName
            : CommandBase<CommandRoleExistsName>
        {
            public static readonly PropertyInfo<string> NameProperty
                             = RegisterProperty<string>(p => p.Name);
            public string Name
            {
                get { return ReadProperty(NameProperty); }
                set { LoadProperty(NameProperty, value); }
            }

            public static readonly PropertyInfo<bool> ExistsProperty
                             = RegisterProperty<bool>(p => p.Exists);
            public bool Exists
            {
                get { return ReadProperty(ExistsProperty); }
                private set { LoadProperty(ExistsProperty, value); }
            }

            public CommandRoleExistsName()
            {
            }

            public CommandRoleExistsName(string name)
            {
                Name = name;
            }

            protected override void DataPortal_Execute()
            {
                using (var ctx = DalFactory.GetManager())
                {
                    var dal = ctx.GetProvider<IRoleDal>();

                    Exists  = dal.ExistsName(Name);
                }
            }
        }

        #endregion
    }
}
