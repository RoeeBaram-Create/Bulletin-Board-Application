using BulletinBoard.Domain.Constants;
using BulletinBoard.Domain.Exceptions;
using BulletinBoard_.Application.Validators.Validations.Interfaces;

namespace BulletinBoard_.Application.Validators.Validations
{
    public class TitleLengthValidation: ITitleLengthValidation
    {
        public void Validate(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length < 3)
                throw new InvalidAdException(ErrorMessages.InvalidTitleLength);
        }
    }
}
