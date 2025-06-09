using Shared.Exceptions;

namespace Api.Exceptions
{
    public class ValidationException : AppException
    {
        public ValidationException()
            : base(StatusCodes.Status400BadRequest, "ValidationError", "Error de validación.", []) { }

        public ValidationException(string error)
            : base(StatusCodes.Status400BadRequest, "ValidationError", error, [error]) { }

        public ValidationException(List<string> errors)
            : base(StatusCodes.Status400BadRequest, "ValidationError", "Uno o más errores de validación ocurrieron.", errors) { }

    }
}
