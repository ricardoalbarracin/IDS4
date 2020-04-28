using Skoruba.IdentityServer4.Admin.BusinessLogic.Dtos.EncryptionKey;
using System.Threading.Tasks;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Services.Interfaces
{
    public interface IEncryptionKeyService
    {
        Task<EncryptionKeyDto> GetEncryptionKeyByClientIdAsync(int clientId);

        Task<EncryptionKeyDto> AddEncryptionKeyAsync(EncryptionKeyDto encryptionKeyDto);


    }
}