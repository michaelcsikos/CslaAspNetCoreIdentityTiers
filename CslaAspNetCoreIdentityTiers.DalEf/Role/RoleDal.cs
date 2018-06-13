using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla.Data.EntityFrameworkCore;
using CslaAspNetCoreIdentityTiers.Dal;

namespace CslaAspNetCoreIdentityTiers.DalEf
{
    public class RoleDal
        : IRoleDal
    {
        public List<RoleDto> Fetch()
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var dtos = (from   r in ctx.DbContext.Roles
                            select new RoleDto
                            {
                                Id               = r.Id,
                                Name             = r.Name,
                                NormalizedName   = r.NormalizedName,
                                ConcurrencyStamp = r.ConcurrencyStamp
                            }).ToList();

                return dtos;
            }
        }

        public RoleDto Fetch(Guid id)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var dto = (from   r in ctx.DbContext.Roles
                           where  r.Id == id
                           select new RoleDto
                           {
                               Id               = r.Id,
                               Name             = r.Name,
                               NormalizedName   = r.NormalizedName,
                               ConcurrencyStamp = r.ConcurrencyStamp
                           }).Single();

                return dto;
            }
        }

        public RoleDto Fetch(string normalizedName)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var dto = (from   r in ctx.DbContext.Roles
                           where  r.NormalizedName == normalizedName
                           select new RoleDto
                           {
                               Id               = r.Id,
                               Name             = r.Name,
                               NormalizedName   = r.NormalizedName,
                               ConcurrencyStamp = r.ConcurrencyStamp
                           }).Single();

                return dto;
            }
        }

        public void Insert(RoleDto dto)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var db   = ctx.DbContext;
                var data = new Role();

                SetData(data, dto);

                db.Roles.Add(data);

                db.SaveChanges();

                dto.ConcurrencyStamp = data.ConcurrencyStamp;
            }
        }

        public void Update(RoleDto dto)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var db   = ctx.DbContext;
                var data = new Role();

                SetData(data, dto);

                data.ConcurrencyStamp = dto.ConcurrencyStamp;

                db.AttachAsModified(data);

                db.SaveChanges();

                dto.ConcurrencyStamp = data.ConcurrencyStamp;
            }
        }

        private void SetData(Role data, RoleDto dto)
        {
            data.Id             = dto.Id;
            data.Name           = dto.Name;
            data.NormalizedName = dto.NormalizedName;
        }

        public void Delete(Guid id)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
                ctx.DbContext.DeleteByPK(ctx.DbContext.Roles, id);
        }

        public bool ExistsId(Guid id)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var result = (from   r in ctx.DbContext.Roles
                              where  r.Id == id
                              select r).Any();

                return result;
            }
        }

        public bool ExistsName(string normalizedName)
        {
            using (var ctx = DbContextManager<EfDbContext>.GetManager())
            {
                var result = (from   r in ctx.DbContext.Roles
                              where  r.NormalizedName == normalizedName
                              select r).Any();

                return result;
            }
        }
    }
}
