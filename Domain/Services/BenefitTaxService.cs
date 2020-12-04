using System.Threading.Tasks;
using BenefitTaxApi.API.Contracts;
using BenefitTaxApi.Infrastructure.Interfaces;

namespace BenefitTaxApi.Domain.Services
{
    public class BenefitTaxService : IBenefitTaxService
    {
        private readonly ITaxAgencyClient _taxOfficeClient;
        private readonly ITaxCalculationService _taxCalculationService;

        public BenefitTaxService(ITaxAgencyClient taxOfficeClient, ITaxCalculationService taxCalculationService)
        {
            _taxOfficeClient = taxOfficeClient;
            _taxCalculationService = taxCalculationService;
        }

        public async Task<TaxResponse> CalculateNetCost(BenefitTaxRequest benefitTaxRequest)
        {
            // Get Tax table
            var taxtable = await GetTaxTable(benefitTaxRequest);

            // Get income from and to
            var incomePairNoBenefit = _taxCalculationService.GetIncomeInterval(benefitTaxRequest.Income);
            var incomePairWithBenefit = _taxCalculationService.GetIncomeInterval(benefitTaxRequest.Income + benefitTaxRequest.BenefitTax);

            // Calculate tax to deduct
            var taxToDeductNoBenefit = await _taxOfficeClient.GetTaxToDeduct(taxtable, "30B", incomePairNoBenefit.IncomeTop, benefitTaxRequest.IncomeYear, incomePairNoBenefit.IncomeBottom);
            var taxToDeductWithBenefit = await _taxOfficeClient.GetTaxToDeduct(taxtable, "30B", incomePairWithBenefit.IncomeTop, benefitTaxRequest.IncomeYear, incomePairWithBenefit.IncomeBottom);

            var netIncomeNoBeneFit = benefitTaxRequest.Income - taxToDeductNoBenefit;
            var netIncomeWithBenefit = benefitTaxRequest.Income - taxToDeductWithBenefit;

            // Return net cost
            return new TaxResponse(netIncomeNoBeneFit - netIncomeWithBenefit);
        }

        public async Task<MunicipalitiesResponse> GetMunicipalities(MunicipalitiesRequest municipalitiesRequest)
        {
            var municipalities = await _taxOfficeClient.GetAllMunicipalities();

            return municipalities;
        }

        private async Task<int> GetTaxTable(BenefitTaxRequest benefitTaxRequest)
        {
            if (benefitTaxRequest.ChurchMember == true && !string.IsNullOrEmpty(benefitTaxRequest.Congregation))
            {
                return await _taxOfficeClient.GetChurchMemberTaxTable(benefitTaxRequest.Municipality, benefitTaxRequest.ChurchMember, benefitTaxRequest.Congregation);
            }

            return await _taxOfficeClient.GetTaxTable(benefitTaxRequest.Municipality);
        }
    }
}
