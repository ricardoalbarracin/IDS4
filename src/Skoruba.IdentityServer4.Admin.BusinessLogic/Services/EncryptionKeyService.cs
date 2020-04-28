using Skoruba.AuditLogging.Services;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Dtos.EncryptionKey;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Events.EncryptionKey;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Mappers;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Services.Interfaces;
using Skoruba.IdentityServer4.Admin.EntityFramework.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Services
{
    public class EncryptionKeyService : IEncryptionKeyService
    {
        protected readonly IEncryptionKeyRepository Repository;
        protected readonly IAuditEventLogger AuditEventLogger;

        public EncryptionKeyService(IEncryptionKeyRepository repository, IAuditEventLogger auditEventLogger)
        {
            Repository = repository;
            AuditEventLogger = auditEventLogger;
        }

        public virtual async Task<EncryptionKeyDto> GetEncryptionKeyByClientIdAsync(int clientId)
        {
            var encryptionKey = await Repository.GetEncryptionKeyByClientIdAsync(clientId);
            var encryptionKeyDto = encryptionKey.ToModel();

            await AuditEventLogger.LogEventAsync(new EncryptionKeyRequestedEvent());

            return encryptionKeyDto;
        }

        public virtual async Task<EncryptionKeyDto> AddEncryptionKeyAsync(EncryptionKeyDto encryptionKeyDto)
        {
            var encryptionKeyEntity = encryptionKeyDto.ToEntity();
            var encryptionKey = await Repository.AddEncryptionKeyAsync(encryptionKeyEntity);
            encryptionKeyDto = encryptionKey.ToModel();
            await AuditEventLogger.LogEventAsync(new EncryptionKeyRequestedEvent());
            return encryptionKeyDto;
        }
    }
}
