using System.Collections.Generic;
using System.Threading.Tasks;
using BenefitTaxApi.API.Contracts;

namespace BenefitTaxApi.Infrastructure.Interfaces
{
    public interface ITaxAgencyClient
    {
        public Task<int> GetTaxTable(string municipality, bool churchMemeber);

        public Task<int> GetTaxToDeduct(int tablenr, string numberOfdays, int incomeTo, int incomeYear, int IncomeFrom);

        public Task<MunicipalitiesResponse> GetAllMunicipalities();

        public Task<List<string>> GetAllCongregations(int incomeYear, string municipality);

    }
}
