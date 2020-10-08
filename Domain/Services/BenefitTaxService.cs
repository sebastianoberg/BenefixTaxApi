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
            var taxtable = await _taxOfficeClient.GetTaxTable(benefitTaxRequest.Municipality, benefitTaxRequest.ChurchMember);

            // Get income from and to
            var incomePairNoBenefit = _taxCalculationService.GetIncomeInterval(benefitTaxRequest.Income);
            var incomePairWithBenefit = _taxCalculationService.GetIncomeInterval(benefitTaxRequest.Income + benefitTaxRequest.BenefitTax);

            // Calculate tax to deduct
            var taxToDeductNoBenefit = await _taxOfficeClient.GetTaxToDeduct(taxtable, "30B", incomePairNoBenefit.IncomeTop, 2020, incomePairNoBenefit.IncomeBottom);
            var taxToDeductWithBenefit = await _taxOfficeClient.GetTaxToDeduct(taxtable, "30B", incomePairWithBenefit.IncomeTop, 2020, incomePairWithBenefit.IncomeBottom);

            var netIncomeNoBeneFit = benefitTaxRequest.Income - taxToDeductNoBenefit;
            var netIncomeWithBenefit = benefitTaxRequest.Income - taxToDeductWithBenefit;
            
            // Return net cost
            return new TaxResponse(netIncomeNoBeneFit - netIncomeWithBenefit);
        }
    }
}
