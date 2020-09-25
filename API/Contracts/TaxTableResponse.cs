using Newtonsoft.Json;

namespace BenefitTaxApi.API.Contracts
{
    public class TaxTableResponse
    {
        /// <summary>
        /// Kommunalskatt
        /// </summary>
        [JsonProperty("kommunalskatt")]
        public double MunicipalTax { get; set; }

        /// <summary>
        /// Landstingsskatt
        /// </summary>
        [JsonProperty("landstingsskatt")]
        public double CountryTax { get; set; }

        /// <summary>
        /// Begravningsavgift
        /// </summary>
        [JsonProperty("begravningsavgift")]
        public double BurialTax { get; set; }

        /// <summary>
        /// Kyrkoavgift
        /// </summary>
        [JsonProperty("kyrkoavgift")]
        public double? ChurchTax { get; set; }

        /// <summary>
        /// Skattesats
        /// </summary>
        [JsonProperty("skattesats")]
        public double TaxRate { get; set; }

        // <summary>
        /// Skattetabell
        /// </summary>
        [JsonProperty("skattetabell")]
        public int TaxTable { get; set; }

        // <summary>
        /// SkattetabellPdf
        /// </summary>
        [JsonProperty("skattetabellPdf")]
        public string TaxTablePdf { get; set; }
    }
}