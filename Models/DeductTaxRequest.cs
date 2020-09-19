namespace BenefitTaxApi.Models
{
    public class DeductTaxRequest
    {
        public DeductTaxRequest()
        {
        }

        public string tablenr { get; set;}
        public string numberOfdays { get; set; }
        public int incomeTo { get; set; }
        public int incomeYear { get; set; }
        public int IncomeFrom { get; set;}
    }
}
