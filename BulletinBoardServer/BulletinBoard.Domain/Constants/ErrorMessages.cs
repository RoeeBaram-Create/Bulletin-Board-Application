namespace BulletinBoard.Domain.Constants
{
    public static class ErrorMessages
    {
        public const string NegativePrice = "מחיר מודעה אינו יכול להיות מספר שלילי.";        
        public const string InvalidTitleLength = "כותרת צריכה להיות לפחות בעלת 3 תווים"; 
        
        public const string TitleNotFound = "Resource Not Found";
        public const string TitleValidationError = "Validation Error";
        public const string TitleServerError = "Internal Server Error";
    }
}
