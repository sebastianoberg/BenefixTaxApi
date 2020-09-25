using BenefitTaxApi.Models;

namespace BenefitTaxApi.Domain
{
    public interface ITaxCalculationService
    {
        public IncomePair GetIncomeInterval(int income);
    }
}
