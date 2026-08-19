using CoreBoard_.Domain.Entities;

namespace BulletinBoard_.Application.Validators.Validations.Interfaces
{
    public interface IReqiredFileldsValidation
    {
        void Validate(Ad ad);
    }
}
