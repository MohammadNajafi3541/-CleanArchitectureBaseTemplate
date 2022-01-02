using AutoMapper;
using Domain.Entities;
using WebApi.DTOs;

namespace WebApi.Mapps
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<Contract, ContractDto>();
            CreateMap<ContractDto, Contract>();
        }
    }


    public class ContractMapper
    {
        private IMapper _mapper;

        public ContractMapper(IMapper mapper)
        {
            _mapper = mapper;

        }


        public ContractDto ToDto(Contract contract)
        {
            return _mapper.Map<ContractDto>(contract);
        }


        public Contract ToEntity(ContractDto dto)
        {
            return _mapper.Map<Contract>(dto);
        }


        public Contract ToEntity(ContractDto dto, Contract contract)
        {
            return _mapper.Map(dto, contract);
        }
    }
}
