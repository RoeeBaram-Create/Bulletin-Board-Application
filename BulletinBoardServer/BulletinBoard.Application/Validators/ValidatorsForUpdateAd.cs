using BulletinBoard_.Application.Validators.Interfaces;
using BulletinBoard_.Application.Validators.Validations.Interfaces;
using CoreBoard_.Domain.Entities;

namespace BulletinBoard_.Application.Validators
{
    public class ValidatorsForUpdateAd : IValidatorsForUpdateAd
    {
        private readonly IReqiredFileldsValidation _reqiredFileldsValidation;
        private readonly ITitleLengthValidation _titleLengthValidation;
        private readonly IPriceValidation _priceValidation;

        public ValidatorsForUpdateAd(
            ITitleLengthValidation titleLengthValidation,
            IPriceValidation priceValidation,
            IReqiredFileldsValidation reqiredFileldsValidation)
        {
            _titleLengthValidation = titleLengthValidation;
            _priceValidation = priceValidation;
            _reqiredFileldsValidation = reqiredFileldsValidation;
        }

        public void Validate(Ad ad)
        {
            _reqiredFileldsValidation.Validate(ad);
            _titleLengthValidation.Validate(ad.Title);
            _priceValidation.Validate(ad.Price);
        }
    }
}
