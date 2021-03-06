using AutoMapper;
using ReducedUserBL = BusinessLayer.Models.ReducedUser;
using UserWithStatisticBL = BusinessLayer.Models.UserWithStatistic;
using UpdatedUserBL = BusinessLayer.Models.UpdatedUser;
using ReducedUserDAL = DataAcessLayer.Models.UserModels.ReducedUser;
using UserWithStatisticDAL = DataAcessLayer.Models.UserModels.UserWithStatistic;
using UpdatedUserDAL = DataAcessLayer.Models.UserModels.UpdatedFullUser;

namespace BusinessLayer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ReducedUserBL, ReducedUserDAL>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(b => b.Id))
                .ForMember(d => d.PasswordSalt, opt => opt.MapFrom(b => b.IndividualSalt))
                .ForMember(d => d.UserLogo, opt => opt.MapFrom(b => b.Logo));

            CreateMap<ReducedUserDAL, ReducedUserBL>()
                .ForMember(b => b.Id, opt => opt.MapFrom(d => d.UserId))
                .ForMember(b => b.IndividualSalt, opt => opt.MapFrom(d => d.PasswordSalt))
                .ForMember(b => b.Logo, opt => opt.MapFrom(d => d.UserLogo));

            CreateMap<UserWithStatisticBL, UserWithStatisticDAL>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(b => b.Id))
                .ForMember(d => d.PasswordSalt, opt => opt.MapFrom(b => b.IndividualSalt))
                .ForMember(d => d.UserLogo, opt => opt.MapFrom(b => b.Logo));

            CreateMap<UserWithStatisticDAL, UserWithStatisticBL>()
                .ForMember(b => b.Id, opt => opt.MapFrom(d => d.UserId))
                .ForMember(b => b.IndividualSalt, opt => opt.MapFrom(d => d.PasswordSalt))
                .ForMember(b => b.Logo, opt => opt.MapFrom(d => d.UserLogo));

            CreateMap<UpdatedUserBL, UpdatedUserDAL>()
                .ForMember(b => b.UserId, opt => opt.MapFrom(d => d.Id));
        }
    }
}
