using System.ComponentModel.DataAnnotations;

namespace BenefitTaxApi.API.Contracts
{
    public class BenefitTaxRequest
    {
        /// <summary>
        /// Kommun
        /// </summary>
        [Required]
        public string Municipality { get; set; }

        /// <summary>
        /// Månadslön
        /// </summary>
        [Range(1, 80000)]
        public int Income { get; set; }

        /// <summary>
        /// Förmånsvärde bil
        /// </summary>
        [Range(1, 30000)]
        public int BenefitTax { get; set; }

        /// <summary>
        /// Medlem i kyrkan
        /// </summary>
        [Required]
        public bool ChurchMember { get; set; }

        /// <summary>
        /// Församling, om medlem i kyrkan
        /// </summary>
        public string Congregation { get; set; }
    }
}
