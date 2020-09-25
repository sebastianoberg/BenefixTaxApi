namespace BenefitTaxApi.Models
{
    public class IncomePair
    {
        public IncomePair(int incomeTop, int incomeBottom)
        {
            IncomeTop = incomeTop;
            IncomeBottom = incomeBottom;
        }

        public int IncomeTop { get; set; }
        public int IncomeBottom { get; set; }
    }
}
