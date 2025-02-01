using AutoMapper;
using DTOs.AppUser;
using Entity.Concrete;

namespace UI.AutoMapping
{
    public class AppUserMapping : Profile
    {
        public AppUserMapping()
        {
            CreateMap<AppUser, AppUserLoginDto>().ReverseMap();
            CreateMap<AppUser, AppUserAddDto>().ReverseMap();
        }
    }
}
