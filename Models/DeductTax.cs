namespace BenefitTaxApi.Models
{
    public class DeductTax
    {
        public int tableNumber { get; set; }
        public string numberOfDays { get; set; }
        public int maxIncome { get; set; }
        public int fromIncome { get; set; }
        public int year { get; set; }
    }
}
