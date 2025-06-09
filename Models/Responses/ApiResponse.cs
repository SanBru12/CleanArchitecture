using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(int statusCode, string message, T? data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        // Métodos de fábrica para conveniencia
        public static ApiResponse<T> Success(T data, string message = "Success", int statusCode = 200)
            => new(statusCode, message, data);

        public static ApiResponse<T> Empty(string message = "No content", int statusCode = 204)
            => new(statusCode, message);

        public static ApiResponse<T> Failure(string message, int statusCode = 400)
            => new(statusCode, message);
    }
}
