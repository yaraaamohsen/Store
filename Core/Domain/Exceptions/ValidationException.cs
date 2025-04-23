namespace Domain.Exceptions
{
    public class ValidationException(IEnumerable<string> Errors) : Exception("Validation Errors")
    {
        public IEnumerable<string> Errors { get; } = Errors;
    }
}
