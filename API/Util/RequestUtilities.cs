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

        public static async Task<BenefitTaxRequest> ValidatebenfitTaxRequestAsync(ITaxAgencyClient _taxOfficeClient, BenefitTaxRequest request)
        {
            if (request == null)
            {
                throw new ValidationException("Request cannot be null", nameof(request));
            }

            if (request.ChurchMember != false)
            {
                var validatedCongregation = await ValidateCongregation(_taxOfficeClient, request.Congregation, 2020, request.Municipality);
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

        public static async Task<CongregationsRequest> ValidateCongregationsRequest(ITaxAgencyClient _taxOfficeClient, CongregationsRequest request)
        {
            if (request == null)
            {
                throw new ValidationException("Request cannot be null", nameof(request));
            }

            request.IncomeYear = ValidateIncomeYear(request.IncomeYear);
            request.Municipality = await RequestUtilites.ValidateMunicipality(_taxOfficeClient, request.Municipality);

            return request;
        }

        private static async Task<string> ValidateMunicipality(ITaxAgencyClient _taxOfficeClient, string municipality)
        {
            var municipalities = (await _taxOfficeClient.GetAllMunicipalities());

            if (!municipalities.Municipality.Contains(municipality))
            {
                throw new ApiException("Municipality not found", HttpStatusCode.NotFound);
            }

            return municipality;
        }

        private static async Task<string> ValidateCongregation(ITaxAgencyClient _taxOfficeClient, string congregation, int incomeYear, string municipality)
        {
            var congregations = (await _taxOfficeClient.GetAllCongregations(incomeYear, municipality));

            if (!congregations.Contains(congregation))
            {
                throw new ApiException("Municipality not found", HttpStatusCode.NotFound);
            }

            return congregation;
        }

        private static int ValidateIncomeYear(int incomeYear)
        {
            if (incomeYear != DateTime.Now.Year)
            {
                throw new ApiException("Requested year is not the current year", HttpStatusCode.BadRequest);
            }

            return incomeYear;
        }
    }
}
