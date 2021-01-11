using System.Collections.Generic;
using Newtonsoft.Json;

namespace BenefitTaxApi.API.Contracts
{
    public class MunicipalitiesResponse
    {
        public MunicipalitiesResponse()
        {
        }

        /// <summary>
        /// list på kommuner per år
        /// </summary>
        [JsonProperty("2020")]
        public List<string> Municipality { get; set; }
    }

}
