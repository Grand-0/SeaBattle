using API.Helpers;
using API.Models.Users;
using AutoMapper;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using UserProfileAPI = API.Models.Users.UserProfile;
using UserProfileBL = BusinessLayer.Models.UserProfile;

namespace API.Mapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile(IWebHostEnvironment env, IConfiguration cnf)
        {
            CreateMap<UserWithStatistic, FullUser>()
                .ForMember(d => d.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(d => d.Login, opt => opt.MapFrom(b => b.Login))
                .ForMember(d => d.Email, opt => opt.MapFrom(b => b.Email))
                .ForMember(d => d.LogoPath, opt => opt.MapFrom(b => b.Logo))
                .ForMember(d => d.WinRate, opt => opt.MapFrom(b => b.WinRate))
                .ForMember(d => d.WinBattles, opt => opt.MapFrom(b => b.WinBattles))
                .ForMember(d => d.Battles, opt => opt.MapFrom(b => b.Battles));

            CreateMap<UserProfileBL, UserProfileAPI>()
                .ForMember(d => d.PathToLogo, opt => opt.MapFrom(b => new LocalResourceHelper(env, cnf).GetResource(b.PathToLogo)));
        }
    }
}
