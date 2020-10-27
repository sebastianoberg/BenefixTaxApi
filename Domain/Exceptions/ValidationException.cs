using System;

namespace BenefitTaxApi.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, string paramName) : base(message)
        {
            ParamName = paramName;
        }

        public string ParamName { get; } = string.Empty;
    }
}
