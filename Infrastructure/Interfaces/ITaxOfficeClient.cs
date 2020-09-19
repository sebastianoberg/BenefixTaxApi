using System.Threading.Tasks;
using BenefitTaxApi.Models.Responses;

namespace BenefitTaxApi.Infrastructure.Interfaces
{
    public interface ITaxOfficeClient
    {
        public Task<TaxTableResponse> GetTaxTableFromTaxOffice(string congregation, bool churchMemeber);

        public Task<DeductTaxResponse> GetFromTaxOffice(string tablenr, string numberOfdays, int incomeTo, int incomeYear, int IncomeFrom);
    }
}
