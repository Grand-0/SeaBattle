using API.Mapper.Profiles;
using AutoMapper;

namespace API.Mapper
{
    public class ConfigurationResources
    {
        public static void RegistrationMapperConfiguration(IMapperConfigurationExpression config)
        {
            config.AddProfile(new UserProfile());
        }
    }
}
