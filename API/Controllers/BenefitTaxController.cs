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
        [Route("benefitTax")]
        [ProducesResponseType(typeof(TaxResponse), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<TaxResponse>> GetBenefitTaxAsync([FromBody] BenefitTaxRequest request)
        {
            RequestUtilites.ValidateBenefitTaxRequest(request);

            var benefitTaxSummary = await _beneFitTaxService.CalculateNetCost(request);

            return Ok(new TaxResponse(benefitTaxSummary.NetCost));
        }

        [HttpGet]
        [Route("municipalities")]
        public async Task<ActionResult<MunicipalitiesResponse>> GetMunicipalitiesAsync(MunicipalitiesRequest municipalitiesRequest)
        {
            if (municipalitiesRequest is null)
            {
                throw new System.ArgumentNullException(nameof(municipalitiesRequest));
            }

            var municipalities = await _beneFitTaxService.GetMunicipalities(municipalitiesRequest);

            return municipalities; ;
        }

        /*         [HttpGet]
                [Route("municipalities")]
                public async Task<ActionResult<TaxResponse>> GetCongregationsAsync(CongregationsRequest congregationsRequest)
                {
                    if (congregationsRequest is null)
                    {
                        throw new System.ArgumentNullException(nameof(congregationsRequest));
                    }

                    var congregations = await _beneFitTaxService.GetCongregations(congregationsRequest);

                    return congregations;;
                } */
    }
}
