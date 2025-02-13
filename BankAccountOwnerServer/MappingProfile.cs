using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace BankAccountOwnerServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDTO>();

            CreateMap<Account, AccountDTO>();
            
            CreateMap<OwnerForCreationDTO, Owner>();//notice direction of mapping here

            CreateMap<OwnerForUpdateDTO, Owner>();
        }
    }
}
