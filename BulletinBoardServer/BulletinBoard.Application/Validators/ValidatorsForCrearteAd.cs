using BulletinBoard_.Application.Validators.Interfaces;
using BulletinBoard_.Application.Validators.Validations.Interfaces;
using CoreBoard_.Domain.Entities;

namespace BulletinBoard_.Application.Validators
{
    public class ValidatorsForCrearteAd : IValidatorsForCrearteAd
    {
        private readonly IReqiredFileldsValidation _reqiredFileldsValidation;
        private readonly ITitleLengthValidation _titleLengthValidation;
        private readonly IPriceValidation _priceValidation;

        public ValidatorsForCrearteAd(
            IReqiredFileldsValidation reqiredFileldsValidation,
            ITitleLengthValidation titleLengthValidation,
            IPriceValidation priceValidation)
        {
            _reqiredFileldsValidation = reqiredFileldsValidation;
            _titleLengthValidation = titleLengthValidation;
            _priceValidation = priceValidation;
        }

        public void Validate(Ad ad)
        {
            _reqiredFileldsValidation.Validate(ad);
            _titleLengthValidation.Validate(ad.Title);
            _priceValidation.Validate(ad.Price);
        }
    }
}
