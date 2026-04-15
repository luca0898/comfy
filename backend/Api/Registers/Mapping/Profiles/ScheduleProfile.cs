using AutoMapper;
using Domain.Entities;
using Domain.ViewModel;

namespace Api.Registers.Mapping.Profiles;

public class ScheduleProfile : Profile
{
    public ScheduleProfile()
    {
        CreateMap<Schedule, ScheduleViewModel>().ReverseMap();
    }
}