namespace BenefitTaxApi.Models
{
    public class TaxTableRequest
    {
        public TaxTableRequest()
        {
        }

        public int Income { get; set; }
        public bool ChurchMemeber { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? Congregation { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
