namespace BulletinBoard.Domain.Exceptions
{
    public class InvalidAdException : Exception
    {
        public InvalidAdException(string message) : base(message)
        {
        }
        public InvalidAdException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
