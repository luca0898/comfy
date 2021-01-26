using AutoMapper;
using Comfy.PRODUCT.Entities;
using Comfy.PRODUCT.ViewModel;

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
