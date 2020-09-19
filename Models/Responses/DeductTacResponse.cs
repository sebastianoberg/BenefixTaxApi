using Newtonsoft.Json;

namespace BenefitTaxApi.Models.Responses
{
    public class DeductTaxResponse
    {
        /// <summary>
        /// resultCount
        /// </summary>
        [JsonProperty("resultCount")]
        public double ResultCount { get; set; }

        /// <summary>
        /// offset
        /// </summary>
        [JsonProperty("offset")]
        public double Offset { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        [JsonProperty("limit")]
        public double Limit { get; set; }

        /// <summary>
        /// Skatteresultat
        /// </summary>
        [JsonProperty("results")]
        public Results[] Results { get; set; }
    }

    public class Results
    {
        /// <summary>
        /// Kolum 2
        /// </summary>
        [JsonProperty("kolumn 2")]
        public int ColumnTwo { get; set; }

        /// <summary>
        /// Kolum 3
        /// </summary>
        [JsonProperty("kolumn 3")]
        public int ColumnThree { get; set; }

        /// <summary>
        /// Kolum 4
        /// </summary>
        [JsonProperty("kolumn 4")]
        public int ColumnFour { get; set; }

        /// <summary>
        /// Kolum 5
        /// </summary>
        [JsonProperty("kolumn 5")]
        public int ColumnFive { get; set; }

        /// <summary>
        /// Kolum 6
        /// </summary>
        [JsonProperty("kolumn 6")]
        public int ColumnSix { get; set; }

        /// <summary>
        /// Nummer på skattetabell
        /// </summary>
        [JsonProperty("tabellnr")]
        public string Tabellnr { get; set; }

        /// <summary>
        /// Antal dagar, vanligast är en månad
        /// </summary>
        [JsonProperty("antal dgr")]
        public string NumberOfDAys { get; set; }

        /// <summary>
        /// Inkomst t.o.m.
        /// </summary>
        [JsonProperty("inkomst t.o.m.")]
        public int IncomeMax { get; set; }

        /// <summary>
        /// Inkomstår
        /// </summary>
        [JsonProperty("år")]
        public int Year { get; set; }

        /// <summary>
        /// Inkomst f.r.o.m
        /// </summary>
        [JsonProperty("inkomst fr.o.m.")]
        public int IncomeMinimum { get; set; }

        /// <summary>
        /// Kolumn 1
        /// </summary>
        [JsonProperty("kolumn 1")]
        public int ColumnOne { get; set; }
    }
}