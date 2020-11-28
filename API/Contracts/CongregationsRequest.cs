namespace BenefitTaxApi.API.Contracts
{
    public class CongregationsRequest
    {
        public CongregationsRequest()
        {
        }

        public int IncomeYear { get; set; }
        public string Municipality { get; set; }
    }
}
