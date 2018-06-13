using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CslaAspNetCoreIdentityTiers.Business;

namespace CslaAspNetCoreIdentityTiers.AppService
{
    public class RoleStore
        : IRoleStore<Role>
    {
        #region IRoleStore

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role = await role.SaveAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            await Role.DeleteRoleAsync(role.Id);

            return IdentityResult.Success;
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            var id = ConvertIdFromString(roleId);

            return Role.GetRoleAsync(id);
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            return Role.GetRoleAsync(normalizedRoleName);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.Name = roleName;

            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role = await role.SaveAsync();

            return IdentityResult.Success;
        }

        #endregion

        #region IDisposable

        bool m_disposed;

        protected void ThrowIfDisposed()
        {
            if (m_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public void Dispose()
        {
            m_disposed = true;
        }

        #endregion

        #region Helpers

        public Guid ConvertIdFromString(string id)
        {
            if (id == null)
                return Guid.Empty;

            return Guid.Parse(id);
        }

        #endregion
    }
}
