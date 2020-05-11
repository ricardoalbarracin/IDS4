using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skoruba.IdentityServer4.Admin.EntityFramework.Entities;
using Skoruba.IdentityServer4.Admin.EntityFramework.Extensions.Common;
using Skoruba.IdentityServer4.Admin.EntityFramework.Extensions.Extensions;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;
using Skoruba.IdentityServer4.Admin.EntityFramework.Repositories.Interfaces;

namespace Skoruba.IdentityServer4.Admin.EntityFramework.Repositories
{
    public class EncryptionKeyRepository<TDbContext> : IEncryptionKeyRepository
        where TDbContext : DbContext, IAdminConfigurationDbContext
    {
        protected readonly TDbContext DbContext;

        public EncryptionKeyRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }


        public virtual async Task<EncryptionKey> GetEncryptionKeyByClientIdAsync(int clientId)
        {
            var encryptionKey = await DbContext.EncryptionKeys
                .Where(x => (x.ClientId == clientId) && x.Enable)
                .FirstOrDefaultAsync();
            
            return encryptionKey;
        }

        public virtual async Task<EncryptionKey> AddEncryptionKeyAsync(EncryptionKey encryptionKey)
        {
            await DbContext.EncryptionKeys.AddAsync(encryptionKey);
            await DbContext.SaveChangesAsync();
            return encryptionKey;
        }


        public virtual async Task<EncryptionKey> UpdateEncryptionKeyAsync(EncryptionKey encryptionKey)
        {
            if (encryptionKey != null)
            {
                var result = await DbContext.EncryptionKeys.SingleOrDefaultAsync(b => b.Id == encryptionKey.Id);
                if (result != null)
                {
                    result.Enable = encryptionKey.Enable;
                    result.EncryptionSecret = encryptionKey.EncryptionSecret;
                    result.ClientId = encryptionKey.ClientId;

                    DbContext.SaveChanges();
                }
            }
            return encryptionKey;
        }


        public virtual async Task DeleteEncryptionKeyByClientIdAsync(int clientId)
        {
            var encryptionKeyToDelete = await DbContext.EncryptionKeys.Where(x => x.ClientId == clientId).ToListAsync();

            if (encryptionKeyToDelete.Count == 0) return;

            DbContext.EncryptionKeys.RemoveRange(encryptionKeyToDelete);
            await DbContext.SaveChangesAsync();
        }
    }
}