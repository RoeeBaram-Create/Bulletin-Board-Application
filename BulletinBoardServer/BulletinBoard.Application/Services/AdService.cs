using BulletinBoard.Domain.Exceptions;
using BulletinBoard.Domain.Interfaces;
using BulletinBoard_.Application.Dtos;
using BulletinBoard_.Application.Services.Interfaces;
using BulletinBoard_.Application.Validators.Interfaces;
using CoreBoard_.Domain.Entities;

namespace BulletinBoard_.Application.Services
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;
        private readonly IValidatorsForUpdateAd _validatorsForUpdateAd;
        private readonly IValidatorsForCrearteAd _validatorsForCrearteAd;

        public AdService(
            IAdRepository adRepository,
            IValidatorsForUpdateAd validatorsForUpdateAd,
            IValidatorsForCrearteAd validatorsForCrearteAd)
        {
            _adRepository = adRepository;
            _validatorsForUpdateAd = validatorsForUpdateAd;
            _validatorsForCrearteAd = validatorsForCrearteAd;
        }

        public async Task<IEnumerable<Ad>> GetAdsAsync(AdFilterDto? filter = null)
        {
            return await _adRepository.GetAllAsync(filter);
        }
        public async Task DeleteAdAsync(Guid id)
        {
            bool isDeleted = await _adRepository.DeleteAsync(id);

            if (isDeleted == false)
                throw new AdNotFoundException(id);
        }
        public async Task<Ad?> UpdateAdAsync(Ad ad)
        {
            _validatorsForUpdateAd.Validate(ad);

            var updatedEntity = await _adRepository.UpdateAsync(ad);

            if (updatedEntity == null)
                throw new AdNotFoundException(ad.Id);

            return updatedEntity;
        }
        public async Task<Ad> CreateAdAsync(Ad ad)
        {
            _validatorsForCrearteAd.Validate(ad);

            return await _adRepository.AddAsync(ad);
        }
    }
}
