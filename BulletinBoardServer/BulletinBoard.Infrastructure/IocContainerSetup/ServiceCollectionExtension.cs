using BulletinBoard.Domain.Interfaces;
using BulletinBoard.Infrastructure.Persistance;
using BulletinBoard.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BulletinBoard_.Application.Services.Interfaces;
using BulletinBoard_.Application.Services;
using BulletinBoard_.Application.Validators.Validations.Interfaces;
using BulletinBoard_.Application.Validators.Validations;
using BulletinBoard_.Application.Validators.Interfaces;
using BulletinBoard_.Application.Validators;

namespace JobPosting.Infrastructure.IocContainerSetup
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrusratureServicesCollection(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg =>
            {
            }, typeof(ServiceCollectionExtension).Assembly);

            services.Configure<FileStorageSettings>(
                configuration.GetSection("FileStorageSettings"));

            services.AddSingleton<IAdRepository, JsonAdRepository>();

            AddAdsServices(services);
            AddAdsValidations(services);
        }

        public static void AddAdsServices(this IServiceCollection services)
        {
            services.AddScoped<IAdService, AdService>();
        }

        public static void AddAdsValidations(this IServiceCollection services)
        {
            services.AddScoped<IValidatorsForUpdateAd, ValidatorsForUpdateAd>();
            services.AddScoped<IValidatorsForCrearteAd, ValidatorsForCrearteAd>();
            services.AddScoped<ITitleLengthValidation, TitleLengthValidation>();
            services.AddScoped<IPriceValidation, PriceValidation>();
            services.AddScoped<IReqiredFileldsValidation, ReqiredFileldsValidation>();
        }
    }
}
