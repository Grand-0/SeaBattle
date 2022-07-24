using API.Mapper.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace API.Mapper
{
    public class ConfigurationResources
    {
        public static void RegistrationMapperConfiguration
            (
                IWebHostEnvironment env,
                IConfiguration cnf,
                IMapperConfigurationExpression config
            )
        {
            config.AddProfile(new UserProfile(env, cnf));
        }
    }
}
