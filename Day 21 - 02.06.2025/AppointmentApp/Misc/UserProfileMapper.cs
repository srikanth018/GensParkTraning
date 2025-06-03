using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;
using AutoMapper;

namespace AppointmentApp.Misc
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
        {
            CreateMap<AddDoctorRequest, User>()
            .ForMember(dest => dest.Username, act => act.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<User, AddDoctorRequest>()
            .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Username));
        }
    }
}