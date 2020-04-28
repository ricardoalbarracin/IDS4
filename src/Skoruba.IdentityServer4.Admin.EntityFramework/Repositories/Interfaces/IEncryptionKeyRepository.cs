using System;
using System.Threading.Tasks;
using Skoruba.IdentityServer4.Admin.EntityFramework.Entities;
using Skoruba.IdentityServer4.Admin.EntityFramework.Extensions.Common;

namespace Skoruba.IdentityServer4.Admin.EntityFramework.Repositories.Interfaces
{
    public interface IEncryptionKeyRepository
    {
        Task<EncryptionKey> GetEncryptionKeyByClientIdAsync(int clientId);

        Task<EncryptionKey> AddEncryptionKeyAsync(EncryptionKey encryptionKey);

        Task<EncryptionKey> UpdateEncryptionKeyAsync(EncryptionKey encryptionKey);

        Task DeleteEncryptionKeyByClientIdAsync(int clientId);

    }
}