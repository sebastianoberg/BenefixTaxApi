using System.Threading.Tasks;
using BenefitTaxApi.Infrastructure.Clients;
using BenefitTaxApi.Infrastructure.Interfaces;
using BenefitTaxApi.Models;

namespace BenefitTaxApi.Infrastructure
{
    public class BenefitTaxService
    {
        private readonly ITaxAgencyClient _taxOfficeClient = new TaxAgencyClient();

        public async Task<TaxResponse> CalculateNetCost(BenefitTaxRequest benefitTaxRequest) 
        {
             // Get Tax table
            var taxtable = await _taxOfficeClient.GetTaxTable(benefitTaxRequest.Municipality, benefitTaxRequest.ChurchMember);

            // Get income from and to
            var incomePair = TaxCalculationService.CalculateAmount(benefitTaxRequest.Income);

            // Calculate tax to deduct
            var deductTax = await _taxOfficeClient.GetTaxToDeduct(
                taxtable,
                "30",
                incomePair.IncomeTop,
                2020,
                incomePair.IncomeBottom
                );

            var netcost = benefitTaxRequest.Income - deductTax;
            // Return net cost
            return new TaxResponse(netcost);
        }
    }
}
