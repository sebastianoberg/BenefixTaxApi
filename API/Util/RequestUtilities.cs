using System.Threading.Tasks;
using BenefitTaxApi.API.Contracts;
using BenefitTaxApi.Domain.Exceptions;

namespace BenefitTaxApi.API.Util
{
    internal static class RequestUtilites
    {
        public static BenefitTaxRequest ValidateBenefitTaxRequest(BenefitTaxRequest request)
        {
            if (request == null)
            {
                throw new ValidationException("Request cannot be null", nameof(request));
            }

            return request;
        }
    }
}
