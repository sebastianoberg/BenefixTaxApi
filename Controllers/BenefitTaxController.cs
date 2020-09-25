using System.Threading.Tasks;
using BenefitTaxApi.Infrastructure;
using BenefitTaxApi.Infrastructure.Interfaces;
using BenefitTaxApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BenefitTaxApi.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    [ApiController]
    public class TaxBenefitController : ControllerBase
    {
        private readonly ITaxAgencyClient _taxOfficeClient;
        private readonly BenefitTaxService _beneFitTaxService = new BenefitTaxService();

        public TaxBenefitController(ITaxAgencyClient taxOfficeClient)
        {
            _taxOfficeClient = taxOfficeClient;
        }

        [HttpGet]
        [Route("benefitTax")]
        public async Task<ActionResult<TaxResponse>> GetBenefitTaxAsync(BenefitTaxRequest benefitTaxRequest)
        {
            if (benefitTaxRequest is null)
            {
                throw new System.ArgumentNullException(nameof(benefitTaxRequest));
            }

            var benefitTaxSummary = await _beneFitTaxService.CalculateNetCost(benefitTaxRequest);

            return benefitTaxSummary;;
        }
    }
}
