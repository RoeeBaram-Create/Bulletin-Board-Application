using BulletinBoard.Domain.Constants;
using BulletinBoard.Domain.Exceptions;
using BulletinBoard_.Application.Validators.Validations.Interfaces;
using CoreBoard_.Domain.Entities;

namespace BulletinBoard_.Application.Validators.Validations
{
    public class PriceValidation: IPriceValidation
    {
        public void Validate(decimal? price)
        {
            if (price < 0)
                throw new InvalidAdException(ErrorMessages.NegativePrice);
        }
    }
}
