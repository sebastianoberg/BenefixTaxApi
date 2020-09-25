using System.Threading.Tasks;
using BenefitTaxApi.API.Contracts;
using BenefitTaxApi.Domain;
using BenefitTaxApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BenefitTaxApi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    [ApiController]
    public class TaxBenefitController : ControllerBase
    {
        private readonly ITaxAgencyClient _taxOfficeClient;
        private readonly IBenefitTaxService _beneFitTaxService;

        public TaxBenefitController(ITaxAgencyClient taxOfficeClient, IBenefitTaxService benefitTaxService)
        {
            _taxOfficeClient = taxOfficeClient;
            _beneFitTaxService = benefitTaxService;
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
