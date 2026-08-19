using BulletinBoard.Domain.Exceptions;
using BulletinBoard_.Application.Validators.Validations.Interfaces;
using CoreBoard_.Domain.Entities;

namespace BulletinBoard_.Application.Validators.Validations
{
    public class ReqiredFileldsValidation: IReqiredFileldsValidation
    {
        public void Validate(Ad ad)
        {
            if (string.IsNullOrWhiteSpace(ad.Title))
                throw new InvalidAdException("שדה כותרת חובה");

            if (string.IsNullOrWhiteSpace(ad.City))
                throw new InvalidAdException("שדה מיקום חובה");

            if (ad.Category.HasValue == false)
                throw new InvalidAdException("שדה קטגוריה חובה");

            if (ad.Price.HasValue == false)
                throw new InvalidAdException("שדה מחיר חובה");

            if (string.IsNullOrWhiteSpace(ad.Description))
                throw new InvalidAdException("שדה תיאור מודעה חובה");
        }
    }
}
