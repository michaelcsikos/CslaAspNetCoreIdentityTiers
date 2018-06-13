using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CslaAspNetCoreIdentityTiers.Business;

namespace CslaAspNetCoreIdentityTiers.AppService
{
    public class AppUserStore
        : IUserLoginStore<AppUser>
      //, IUserClaimStore<AppUser>
        , IUserPasswordStore<AppUser>
        , IUserSecurityStampStore<AppUser>
        , IUserEmailStore<AppUser>
        , IUserLockoutStore<AppUser>
        , IUserPhoneNumberStore<AppUser>
      //, IQueryableUserStore<AppUser>
        , IUserTwoFactorStore<AppUser>
      //, IUserAuthenticationTokenStore<AppUser>
        , IUserAuthenticatorKeyStore<AppUser>
        , IUserTwoFactorRecoveryCodeStore<AppUser>
    {
        #region IUserLoginStore

        public Task AddLoginAsync(AppUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user = await user.SaveAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await AppUser.DeleteAppUserAsync(user.Id);

            return IdentityResult.Success;
        }

        public async Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            var id     = ConvertIdFromString(userId);

            var exists = await AppUser.ExistsIdAsync(id);

            if (exists)
                return await AppUser.GetAppUserAsync(id);

            return null;
        }

        public Task<AppUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            var exists = await AppUser.ExistsNormalizedUserNameAsync(normalizedUserName);

            if (exists)
                return await AppUser.GetAppUserAsync(normalizedUserName);

            return null;
        }

        public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.UserName = userName;

            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.CompletedTask;
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(AppUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user = await user.SaveAsync();

            return IdentityResult.Success;
        }

        #endregion

        #region IUserPasswordStore

        public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.PasswordHash != null);
        }

        #endregion

        #region IUserSecurityStampStore

        public Task SetSecurityStampAsync(AppUser user, string stamp, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (stamp == null)
                throw new ArgumentNullException(nameof(stamp));

            user.SecurityStamp = stamp;

            return Task.CompletedTask;
        }

        public Task<string> GetSecurityStampAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }

        #endregion

        #region IUserEmailStore

        public Task SetEmailAsync(AppUser user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.Email = email;

            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(AppUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.EmailConfirmed = confirmed;

            return Task.CompletedTask;
        }

        public async Task<AppUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            var id = await AppUser.GetIdByNormalizedEmailAsync(normalizedEmail);

            if (id != Guid.Empty)
                return await AppUser.GetAppUserAsync(id);

            return null;
        }

        public Task<string> GetNormalizedEmailAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(AppUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.CompletedTask;
        }

        #endregion

        #region IUserLockoutStore

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.LockoutEnd);
        }

        public Task SetLockoutEndDateAsync(AppUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.LockoutEnd = lockoutEnd;

            return Task.CompletedTask;
        }

        public Task<int> IncrementAccessFailedCountAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.AccessFailedCount++;

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.AccessFailedCount = 0;

            return Task.CompletedTask;
        }

        public Task<int> GetAccessFailedCountAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(AppUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.LockoutEnabled = enabled;

            return Task.CompletedTask;
        }

        #endregion

        #region IUserPhoneNumberStore

        public Task SetPhoneNumberAsync(AppUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PhoneNumber = phoneNumber;

            return Task.CompletedTask;
        }

        public Task<string> GetPhoneNumberAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(AppUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PhoneNumberConfirmed = confirmed;

            return Task.CompletedTask;
        }

        #endregion

        #region IUserTwoFactorStore

        public Task SetTwoFactorEnabledAsync(AppUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.TwoFactorEnabled = enabled;

            return Task.CompletedTask;
        }

        public Task<bool> GetTwoFactorEnabledAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.TwoFactorEnabled);
        }

        #endregion

        #region IUserAuthenticationTokenStore - NOT IMPLEMENTED

        public Task SetTokenAsync(AppUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //cancellationToken.ThrowIfCancellationRequested();

            //ThrowIfDisposed();

            //if (user == null)
            //    throw new ArgumentNullException(nameof(user));

            //var token = await FindTokenAsync(user, loginProvider, name, cancellationToken);

            //if (token == null)
            //    await AddUserTokenAsync(CreateUserToken(user, loginProvider, name, value));
            //else
            //    token.Value = value;
        }

        public Task RemoveTokenAsync(AppUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            cancellationToken.ThrowIfCancellationRequested();

            //ThrowIfDisposed();

            //if (user == null)
            //    throw new ArgumentNullException(nameof(user));

            //var entry = await FindTokenAsync(user, loginProvider, name, cancellationToken);

            //if (entry != null)
            //    await RemoveUserTokenAsync(entry);
        }

        public Task<string> GetTokenAsync(AppUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //cancellationToken.ThrowIfCancellationRequested();

            //ThrowIfDisposed();

            //if (user == null)
            //    throw new ArgumentNullException(nameof(user));

            //var entry = await FindTokenAsync(user, loginProvider, name, cancellationToken);

            //if (entry != null)
            //    return entry.Value;

            //return null;
        }

        #endregion

        #region IUserAuthenticatorKeyStore

        const string InternalLoginProvider     = "[AspNetUserStore]",
                     AuthenticatorKeyTokenName = "AuthenticatorKey",
                     RecoveryCodeTokenName     = "RecoveryCodes";

        public Task SetAuthenticatorKeyAsync(AppUser user, string key, CancellationToken cancellationToken)
        {
            return SetTokenAsync(user, InternalLoginProvider, AuthenticatorKeyTokenName, key, cancellationToken);
        }

        public Task<string> GetAuthenticatorKeyAsync(AppUser user, CancellationToken cancellationToken)
        {
            return GetTokenAsync(user, InternalLoginProvider, AuthenticatorKeyTokenName, cancellationToken);
        }

        #endregion

        #region IUserTwoFactorRecoveryCodeStore

        public Task ReplaceCodesAsync(AppUser user, IEnumerable<string> recoveryCodes, CancellationToken cancellationToken)
        {
            var mergedCodes = string.Join(";", recoveryCodes);

            return SetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, mergedCodes, cancellationToken);
        }

        public Task<bool> RedeemCodeAsync(AppUser user, string code, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountCodesAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
