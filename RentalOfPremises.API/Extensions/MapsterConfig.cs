using Mapster;
using MapsterMapper;
using RentalOfPremises.Application.Mapping;
using RentalOfPremises.Infrastructure.Mapping;

namespace RentalOfPremises.API.Extensions
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            // Подключение глобальных настроек Mapster
            //ТУТ НАДО ПРОСКАНИТЬ ВСЮ СБОРКУ И ПОДКЛЮЧИТЬ ВСЕ КОНФИГУРАЦИИ IRegister!!!!!!!!!
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(UserMappingConfig).Assembly); // Сканирует текущую сборку
            config.Scan(typeof(ModelToEntityUserMappingConfig).Assembly);

            // Регистрация IMapper в DI
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}
