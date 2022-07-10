using API.Hubs;
using API.Identity;
using API.HashProtection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;
using ConfigurationResourcesBL = BusinessLayer.ConfigurationResources;
using API.Resources;
using Microsoft.AspNetCore.Http.Connections;
using System.Text;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IJwtGenerator, JwtGenerator>();
            services.AddSingleton<UserStore<string>>();
            services.AddScoped<IHashService, BCryptHashService>();

            services.AddAutoMapper(cnf => {
                ConfigurationResourcesBL.RegistrationMapperConfiguration(cnf);
            });

            ConfigurationResourcesBL.RegistrationServices(services, Configuration.GetConnectionString("DefaultConnection"));

            services.AddCors(c => c.AddPolicy("LocalEnviroment", policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                      .AllowAnyHeader()
                      .AllowCredentials()
                      .AllowAnyMethod();
            }));


            services
                .AddAuthentication(opt => {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));

                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JwtUserSettings.Issuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = true,
                        ValidAudience = JwtUserSettings.Audience,
                    };

                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/hubs")))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        },

                        OnTokenValidated = context =>
                        {
                            var te = context.SecurityToken;
                            return Task.CompletedTask;
                        },

                        OnAuthenticationFailed = context =>
                        {
                            var te = context.Result;
                            return Task.CompletedTask;
                        }
                    };

                });

            services.AddSignalR();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("LocalEnviroment");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<MenuHub>("/hubs/menu", opt => {
                    opt.Transports = HttpTransportType.WebSockets; 
                });
            });
        }
    }
}
