using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BenefitTaxApi.API.Contracts;
using BenefitTaxApi.API.Util;
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
        [Route("calculateBenefitTaxNetCost")]
        [ProducesResponseType(typeof(TaxResponse), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<TaxResponse>> GetBenefitTaxAsync([FromBody] BenefitTaxRequest request)
        {
            await RequestUtilites.ValidatebenfitTaxRequestAsync(_taxOfficeClient, request);

            var benefitTaxSummary = await _beneFitTaxService.CalculateNetCost(request);

            return Ok(new TaxResponse(benefitTaxSummary.NetCost));
        }

        [HttpGet]
        [Route("municipalities")]
        public async Task<ActionResult<MunicipalitiesResponse>> GetMunicipalitiesAsync(MunicipalitiesRequest request)
        {
            RequestUtilites.ValidateMunicipalitiesRequest(request);

            var municipalities = await _beneFitTaxService.GetMunicipalities(request);

            return municipalities; ;
        }

        [HttpGet]
        [Route("congregations")]
        public async Task<ActionResult<List<string>>> GetCongregationsAsync(CongregationsRequest request)
        {
            var validRequest = RequestUtilites.ValidateCongregationsRequest(_taxOfficeClient, request);
            var congregations = await _taxOfficeClient.GetAllCongregations(validRequest.Result.IncomeYear, validRequest.Result.Municipality);

            return congregations;
        }
    }
}
