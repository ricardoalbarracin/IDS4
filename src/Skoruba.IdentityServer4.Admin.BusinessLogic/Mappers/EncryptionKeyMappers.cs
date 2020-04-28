using AutoMapper;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Dtos.EncryptionKey;
using Skoruba.IdentityServer4.Admin.EntityFramework.Entities;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Mappers
{
    public static class EncryptionKeyMappers
    {
        internal static IMapper Mapper { get; }

        static EncryptionKeyMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<EncryptionKeyMapperProfile>())
                .CreateMapper();
        }

        public static EncryptionKeyDto ToModel(this EncryptionKey EncryptionKey)
        {
            return Mapper.Map<EncryptionKeyDto>(EncryptionKey);
        }
       
        public static EncryptionKey ToEntity(this EncryptionKeyDto EncryptionKey)
        {
            return Mapper.Map<EncryptionKey>(EncryptionKey);
        }
    }
}
