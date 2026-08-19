using CoreBoard_.Domain.Entities;

namespace BulletinBoard_.Application.Validators.Validations.Interfaces
{
    public interface IPriceValidation
    {
        public void Validate(decimal? price);
    }
}
