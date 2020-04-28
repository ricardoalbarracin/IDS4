using AutoMapper;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Dtos.EncryptionKey;
using Skoruba.IdentityServer4.Admin.EntityFramework.Entities;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Mappers
{
    public class EncryptionKeyMapperProfile : Profile
    {
        public EncryptionKeyMapperProfile()
        {
            CreateMap<EncryptionKey, EncryptionKeyDto>(MemberList.Destination)
                .ReverseMap();
            
            
        }
    }
}
