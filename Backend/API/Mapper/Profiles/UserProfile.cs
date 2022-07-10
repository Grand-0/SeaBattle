using API.Models.Users;
using AutoMapper;
using BusinessLayer.Models;

namespace API.Mapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserWithStatistic, FullUser>()
                .ForMember(d => d.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(d => d.Login, opt => opt.MapFrom(b => b.Login))
                .ForMember(d => d.Email, opt => opt.MapFrom(b => b.Email))
                .ForMember(d => d.LogoPath, opt => opt.MapFrom(b => b.Logo))
                .ForMember(d => d.WinRate, opt => opt.MapFrom(b => b.WinRate))
                .ForMember(d => d.WinBattles, opt => opt.MapFrom(b => b.WinBattles))
                .ForMember(d => d.Battles, opt => opt.MapFrom(b => b.Battles));


        }
    }
}
