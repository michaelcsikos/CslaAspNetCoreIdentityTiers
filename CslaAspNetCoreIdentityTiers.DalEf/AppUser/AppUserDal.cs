using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla.Data.EntityFrameworkCore;
using CslaAspNetCoreIdentityTiers.Dal;

namespace CslaAspNetCoreIdentityTiers.DalEf
{
    public class AppUserDal
        : IAppUserDal
    {
        public AppUserDto Fetch(Guid id)
        {
            return Fetch(id, null);
        }

        public AppUserDto Fetch(string normalizedUserName)
        {
            return Fetch(null, normalizedUserName);
        }

        private AppUserDto Fetch(Guid? id, string normalizedUserName)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var dto = (from   u in ctx.DbContext.AppUsers
                           where  (!id.HasValue     || u.Id == id)
                              &&  (normalizedUserName == null || u.NormalizedUserName == normalizedUserName)
                           select new AppUserDto
                           {
                               Id                   = u.Id,
                               UserName             = u.UserName,
                               NormalizedUserName   = u.NormalizedUserName,
                               Email                = u.Email,
                               NormalizedEmail      = u.NormalizedEmail,
                               EmailConfirmed       = u.EmailConfirmed,
                               PasswordHash         = u.PasswordHash,
                               SecurityStamp        = u.SecurityStamp,
                               PhoneNumber          = u.PhoneNumber,
                               PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                               TwoFactorEnabled     = u.TwoFactorEnabled,
                               LockoutEnd           = u.LockoutEnd,
                               LockoutEnabled       = u.LockoutEnabled,
                               AccessFailedCount    = u.AccessFailedCount,
                               ConcurrencyStamp     = u.ConcurrencyStamp
                           }).First();

                return dto;
            }
        }

        public void Insert(AppUserDto dto)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var db   = ctx.DbContext;
                var data = new AppUser();

                SetData(data, dto);

                db.AppUsers.Add(data);

                db.SaveChanges();

                dto.ConcurrencyStamp = data.ConcurrencyStamp;
            }
        }

        public void Update(AppUserDto dto)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var db   = ctx.DbContext;
                var data = new AppUser();

                SetData(data, dto);

                data.ConcurrencyStamp = dto.ConcurrencyStamp;

                db.AttachAsModified(data);

                db.SaveChanges();

                dto.ConcurrencyStamp = data.ConcurrencyStamp;
            }
        }

        private void SetData(AppUser data, AppUserDto dto)
        {
            data.Id                   = dto.Id;
            data.UserName             = dto.UserName;
            data.NormalizedUserName   = dto.NormalizedUserName;
            data.Email                = dto.Email;
            data.NormalizedEmail      = dto.NormalizedEmail;
            data.EmailConfirmed       = dto.EmailConfirmed;
            data.PasswordHash         = dto.PasswordHash;
            data.SecurityStamp        = dto.SecurityStamp;
            data.PhoneNumber          = dto.PhoneNumber;
            data.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
            data.TwoFactorEnabled     = dto.TwoFactorEnabled;
            data.LockoutEnd           = dto.LockoutEnd;
            data.LockoutEnabled       = dto.LockoutEnabled;
            data.AccessFailedCount    = dto.AccessFailedCount;
        }

        public void Delete(Guid id)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
                ctx.DbContext.DeleteByPK(ctx.DbContext.AppUsers, id);
        }

        public bool ExistsId(Guid id)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var result = (from   u in ctx.DbContext.AppUsers
                              where  u.Id == id
                              select u).Any();

                return result;
            }
        }

        public bool ExistsNormalizedUserName(string normalizedUserName)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var result = (from   u in ctx.DbContext.AppUsers
                              where  u.NormalizedUserName == normalizedUserName
                              select u).Any();

                return result;
            }
        }

        public Guid GetIdByNormalizedEmail(string normalizedEmail)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var result = (from   u in ctx.DbContext.AppUsers
                              where  u.NormalizedEmail == normalizedEmail
                              select u.Id).FirstOrDefault();

                return result;
            }
        }
    }
}
