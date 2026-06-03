using AutoMapper;
using TestTask.Data.Contract.Model;
using TestTask.Infrastructure.Contact.Model;

namespace TestTask.Infrastructure.Mapping;

internal sealed class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<DataCandidate, Candidate>().ReverseMap();
        CreateMap<DataUser, User>().ReverseMap();
    }
}
