namespace Domain.Exceptions
{
    public class UnAuthorizedException(string Message = "Invalid Email Or Password") : Exception(Message)
    {
    }
}
