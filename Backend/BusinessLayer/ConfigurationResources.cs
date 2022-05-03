using AutoMapper;
using BusinessLayer.Services.UserService;
using DataAcessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer
{
    public static class ConfigurationResources
    {
        public static void RegistrationServices(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUnitOfWork>(o => new UnitOfWork(connectionString));
            services.AddTransient<IUserService, UserService>();
        }

        public static void RegistrationMapperConfiguration(IMapperConfigurationExpression config)
        {

        }
    }
}
