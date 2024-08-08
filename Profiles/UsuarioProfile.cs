using AutoMapper;
using ecoshare_backend.Data.DTOs;
using ecoshare_backend.Models;

namespace ecoshare_backend.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() 
        {
            CreateMap<UserRegistrationDTO, Usuario>();
        }
    }
}
