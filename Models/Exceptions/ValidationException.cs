namespace Infrastructure.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("Error de validación."){ }

        public ValidationException(string error) : base(error) { }

    }
}
