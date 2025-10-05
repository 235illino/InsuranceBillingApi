using AutoMapper;
using InsuranceBillingApi.Dtos;
using InsuranceBillingApi.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Client ↔ ClientDto
        CreateMap<Client, ClientDto>().ReverseMap();

        // Client ↔ ClientWithPoliciesDto
        CreateMap<Client, ClientWithPoliciesDto>();

        // InsurancePolicy ↔ InsurancePolicyDto
        CreateMap<InsurancePolicy, InsurancePolicyDto>().ReverseMap();
    }
}
