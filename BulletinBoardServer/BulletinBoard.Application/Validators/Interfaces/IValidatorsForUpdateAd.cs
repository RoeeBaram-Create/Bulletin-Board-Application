using CoreBoard_.Domain.Entities;

namespace BulletinBoard_.Application.Validators.Interfaces
{
    public interface IValidatorsForUpdateAd
    {
        public void Validate(Ad ad);
    }
}
