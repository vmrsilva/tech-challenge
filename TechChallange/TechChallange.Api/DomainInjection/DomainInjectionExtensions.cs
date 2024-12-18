using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechChallange.Api.Mapper;
using TechChallange.Domain.Base.Repository;
using TechChallange.Domain.Contact.Repository;
using TechChallange.Domain.Contact.Service;
using TechChallange.Domain.Region.Repository;
using TechChallange.Domain.Region.Service;
using TechChallange.Infrastructure.Context;
using TechChallange.Infrastructure.Repository.Base;
using TechChallange.Infrastructure.Repository.Contact;
using TechChallange.Infrastructure.Repository.Region;

namespace TechChallange.Api.DomainInjection
{
    public static class DomainInjectionExtensions
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureContext(services, configuration);
            ConfigureBase(services);
            ConfigureRegion(services);
            ConfigureContact(services);
            AddMapper(services, configuration);

            return services;
        }

        public static void ConfigureContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TechChallangeContext>(options => options.UseSqlServer(configuration.GetConnectionString("Database")));
        }

        public static void ConfigureBase(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
        public static void ConfigureRegion(IServiceCollection services)
        {
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IRegionService, RegionService>();
        }

        public static void ConfigureContact(IServiceCollection services)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();
        }

        public static IServiceCollection AddMapper(this IServiceCollection services, IConfiguration configuration)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

    }
}
