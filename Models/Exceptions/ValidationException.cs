using Shared.Exceptions;

namespace Api.Exceptions
{
    public class ValidationException : AppException
    {
        public ValidationException()
            : base(400, "ValidationError", "Error de validación.", []) { }

        public ValidationException(string error)
            : base(400, "ValidationError", error, [error]) { }

        public ValidationException(List<string> errors)
            : base(400, "ValidationError", "Uno o más errores de validación ocurrieron.", errors) { }

    }
}
