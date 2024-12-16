using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentalOfPremises.API.Extensions;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Auth;
using RentalOfPremises.Application.Services;
using RentalOfPremises.Infrastructure.Auth;
using RentalOfPremises.Infrastructure.ImageStorage;
using RentalOfPremises.Infrastructure.MSSQLServer;
using RentalOfPremises.Infrastructure.Repositories;

namespace RentalOfPremises.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Добавление аутентификации
            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(nameof(JWTOptions)));
            var jwtOptions = builder.Configuration.GetSection(nameof(JWTOptions)).Get<JWTOptions>();
            builder.Services.AddAuth(Options.Create(jwtOptions));


            //Добавление конфигураций маппинга Mapster
            builder.Services.RegisterMapsterConfiguration();

            

            // Чтение конфигурации из appsettings.json
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
            
            // Регистрируем сервис работы с изображениями
            builder.Services.AddSingleton<IImageStorage, CloudinaryImageStorage>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //DB context
            builder.Services.AddDbContext<IRentalOfPremisesDbContext, RentalOfPremisesDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



            //Регистрация зависимостей
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IJWTProvider, JWTProvider>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<ICurrentUserContext, CurrentUserContext>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IPremisesService, PremisesService>();
            builder.Services.AddScoped<IPremisesRepository, PremisesRepository>();

            //builder.Services.AddTransient<IImageStorage, CloudinaryImageStorage>();

            /*//Явные лимиты для обработки файлов
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10485760; // 10 MB
            });*/


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseCors("AllowAll");

            app.UseMiddleware<ExceptionHandlingMiddleware>();


            app.UseRouting();
            app.UseHttpsRedirection();


            app.UseCookiePolicy(
                new CookiePolicyOptions
                {
                    MinimumSameSitePolicy = SameSiteMode.Strict,
                    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
                    Secure = CookieSecurePolicy.Always
                });


            app.UseAuthentication();
            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}