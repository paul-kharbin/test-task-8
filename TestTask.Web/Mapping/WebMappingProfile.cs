using System;
using AutoMapper;
using TestTask.Infrastructure.Contact.Model;
using TestTask.Web.Models;

namespace TestTask.Web.Mapping;

internal sealed class WebMappingProfile : Profile
{
    public WebMappingProfile()
    {
        CreateMap<CandidateAddRequest, Candidate>();
        CreateMap<Candidate, CandidateResponse>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => GetAge(src.BirthDate)));
    }

    private static int GetAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;

        return age;
    }
}
