using AutoMapper;
using Domain.Entities;
using Shared.DataTransferObjects;

namespace CrudOperations.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDto>().ForCtorParam("FullName", options => options.MapFrom(newName => string.Join(' ',"Dr.",newName.Name)))
                .ForCtorParam("FullAddress", options => options.MapFrom(fa => string.Join(' ', fa.Address, fa.Country)));

            CreateMap<Account, AccountDto>()
                .ForCtorParam("CreatedAt", options => options.MapFrom(d => d.CreatedAt.ToString("MM/dd/yyyy")));

            CreateMap<OwnerCreationDto, Owner>();

            CreateMap<AccountCreationDto, Account>();

            CreateMap<AccountUpdateDto, Account>();

            CreateMap<OwnerUpdateDto, Owner>();
            
            CreateMap<AccountUpdateDto, Account>().ReverseMap();
        }
    }
}