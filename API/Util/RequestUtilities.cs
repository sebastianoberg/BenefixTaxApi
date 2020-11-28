using System.Threading.Tasks;
using BenefitTaxApi.API.Contracts;
using BenefitTaxApi.Domain.Exceptions;
using BenefitTaxApi.Infrastructure.Interfaces;
using System.Net;
using System;

namespace BenefitTaxApi.API.Util
{
    internal static class RequestUtilites
    {
        public static BenefitTaxRequest ValidatebenfitTaxRequest(BenefitTaxRequest request)
        {
            if (request == null)
            {
                throw new ValidationException("Request cannot be null", nameof(request));
            }

            return request;
        }

        public static MunicipalitiesRequest ValidateMunicipalitiesRequest(MunicipalitiesRequest request)
        {
            if (request == null)
            {
                throw new ValidationException("Request cannot be null", nameof(request));
            }

            return request;
        }

        public static async Task<string> ValidateMunicipality(ITaxAgencyClient _taxOfficeClient, CongregationsRequest request)
        {
            if (request == null)
            {
                throw new ValidationException("Request cannot be null", nameof(request));
            }

            var municipalities = (await _taxOfficeClient.GetAllMunicipalities());
            if (!municipalities.Municipality.Contains(request.Municipality))
            {
                throw new ApiException("Municipality not found", HttpStatusCode.NotFound);
            }

            return request.Municipality;
        }

        public static async Task<int> ValidateIncomeYear(CongregationsRequest request)
        {
            if (request == null)
            {
                throw new ValidationException("Request cannot be null", nameof(request));
            }

            if (request.IncomeYear != DateTime.Now.Year)
            {
                throw new ApiException("Requested year is not the current year", HttpStatusCode.BadRequest);
            }

            return request.IncomeYear;
        }
    }
}
