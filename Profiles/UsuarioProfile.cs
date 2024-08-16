using AutoMapper;
using ecoshare_backend.Data.DTOs;
using ecoshare_backend.Models;

namespace ecoshare_backend.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() 
        {
            CreateMap<UserRegistrationDTO, Usuario>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Telefone))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
