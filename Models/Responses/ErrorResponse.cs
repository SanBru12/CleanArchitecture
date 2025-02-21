﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class ErrorResponse 
    {
        public int StatusCode { get; set; }
        public List<string>? Messages { get; set; }
        public string InternalError { get; set; }

        public ErrorResponse(int statusCode, string internalError, List<string>? messages = null)
        {
            StatusCode = statusCode;
            InternalError = internalError;
            Messages = messages ?? [];
        }
    }
}
