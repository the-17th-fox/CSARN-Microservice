namespace CSARN.SharedLib.Constants.CustomExceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string? message) : base(message)
        {
        }
    }
}
