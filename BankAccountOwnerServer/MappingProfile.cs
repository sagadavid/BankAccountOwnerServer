using AutoMapper;

namespace BankAccountOwnerServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Models.Owner, Entities.DTOs.OwnerDTO>();
            CreateMap<Entities.Models.Account, Entities.DTOs.AccountDTO>();
        }
    }
}
