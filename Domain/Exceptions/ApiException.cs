using System;
using System.Net;

namespace BenefitTaxApi.Domain.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public ApiException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
        {
            StatusCode = (int)statusCode;
        }

        public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}
