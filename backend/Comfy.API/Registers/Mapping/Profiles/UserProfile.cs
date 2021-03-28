using AutoMapper;
using Comfy.Product.Entities;
using Comfy.Product.ViewModel;

namespace Comfy.API.Registers.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
