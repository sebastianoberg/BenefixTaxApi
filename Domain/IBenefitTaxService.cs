using System.Threading.Tasks;
using BenefitTaxApi.API.Contracts;

namespace BenefitTaxApi.Domain
{
    public interface IBenefitTaxService
    {
        public Task<TaxResponse> CalculateNetCost(BenefitTaxRequest benefitTaxRequest);
    }
}
