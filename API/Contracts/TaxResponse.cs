namespace BenefitTaxApi.API.Contracts
{
    public class TaxResponse
    {
        public TaxResponse(int netCost)
        {
            NetCost = netCost;
        }

        /// <summary>
        /// Nettokostnad för bilförmån
        /// </summary>
        public int NetCost { get; set; }
    }
}
