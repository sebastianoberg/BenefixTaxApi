namespace BenefitTaxApi.API.Contracts
{
    public class BenefitTaxRequest
    {
        /// <summary>
        /// Kommun
        /// </summary>
        public string Municipality { get; set;}
        
        /// <summary>
        /// Månadslön
        /// </summary>
        public int Income { get; set; }

        /// <summary>
        /// Förmånsvärde bil
        /// </summary>
        public int BenefitTax { get; set; }

        /// <summary>
        /// Medlem i kyrkan
        /// </summary>
        public bool ChurchMember { get; set; }

        /// <summary>
        /// Församling, om medlem i kyrkan
        /// </summary>
        public string Congregation { get; set; }
    }
}
