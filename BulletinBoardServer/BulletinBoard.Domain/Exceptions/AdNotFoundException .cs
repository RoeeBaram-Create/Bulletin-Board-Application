namespace BulletinBoard.Domain.Exceptions
{
    public class AdNotFoundException : Exception
    {
        public AdNotFoundException(Guid adId)
            : base($"המודעה עם מזהה {adId} לא נמצאה במערכת.")
        {
        }
    }
}

