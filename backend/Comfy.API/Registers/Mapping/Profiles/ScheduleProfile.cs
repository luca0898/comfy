using AutoMapper;
using Comfy.Product.Entities;
using Comfy.Product.ViewModel;

namespace Comfy.Registers.Mapping
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleViewModel>().ReverseMap();
        }
    }
}
