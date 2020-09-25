using System.Threading.Tasks;

namespace BenefitTaxApi.Infrastructure.Interfaces
{
    public interface ITaxAgencyClient
    {
        public Task<int> GetTaxTable(string municipality, bool churchMemeber);

        public Task<int> GetTaxToDeduct(int tablenr, string numberOfdays, int incomeTo, int incomeYear, int IncomeFrom);
    }
}
