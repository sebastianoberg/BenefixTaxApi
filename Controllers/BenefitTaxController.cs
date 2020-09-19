using System.Threading.Tasks;
using BenefitTaxApi.Infrastructure;
using BenefitTaxApi.Infrastructure.Interfaces;
using BenefitTaxApi.Models;
using BenefitTaxApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BenefitTaxApi.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    [ApiController]
    public class TaxBenefitController : ControllerBase
    {
        private readonly ITaxOfficeClient _taxOfficeClient;

        public TaxBenefitController(ITaxOfficeClient taxOfficeClient)
        {
            _taxOfficeClient = taxOfficeClient;
        }

        [HttpGet]
        [Route("taxtable")]
        public async Task<ActionResult<TaxTableResponse>> GetTaxTableAsync(TaxTableRequest taxTableRequest)
        {
            var congregation = taxTableRequest.Congregation;
            var churchMemeber = taxTableRequest.ChurchMemeber;

            var taxtable = await _taxOfficeClient.GetTaxTableFromTaxOffice(congregation, churchMemeber);

            if (taxtable == null)
            {
                return NotFound();
            }

            return taxtable;
        }

        [HttpGet]
        [Route("taxdeduct")]
        public async Task<ActionResult<DeductTaxResponse>> GetTaxToDeductAsync(DeductTaxRequest deductTaxRequest)
        {
            var deductTax = await _taxOfficeClient.GetFromTaxOffice(
                deductTaxRequest.tablenr,
                deductTaxRequest.numberOfdays,
                deductTaxRequest.incomeTo,
                deductTaxRequest.incomeYear,
                deductTaxRequest.IncomeFrom
                );

            var test = TaxCalculationService.CalculateAmount(25221);
            
            return deductTax;
        }
    }
}
