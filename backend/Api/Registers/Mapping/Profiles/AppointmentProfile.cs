using AutoMapper;
using Domain.Entities;
using Domain.ViewModel;

namespace Api.Registers.Mapping.Profiles;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<Appointment, AppointmentViewModel>().ReverseMap();
    }
}