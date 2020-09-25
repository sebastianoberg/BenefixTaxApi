using System.Threading.Tasks;
using BenefitTaxApi.Infrastructure.Interfaces;
using BenefitTaxApi.Models.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace BenefitTaxApi.Infrastructure.Clients
{
    public class TaxAgencyClient : ITaxAgencyClient
    {
        public TaxAgencyClient()
        {
        }

        public async Task<int> GetTaxTable(string municipality, bool churchMemeber)
        {
            return await GetTaxTableFromTaxOffice(municipality, churchMemeber);
        }

        public async Task<int> GetTaxToDeduct(int tablenr, string numberOfdays, int incomeTo, int incomeYear, int IncomeFrom)
        {
            return await GetTaxToDeductFromTaxAgency(tablenr, numberOfdays, incomeTo, incomeYear, IncomeFrom);
        }

        public async Task<int> GetTaxTableFromTaxOffice(string municipality, bool churchMemeber)
        {
            var baseUrl = $"https://www.skatteverket.se/st-api/rest/v1/skattetabell?forsamling=&inkomstar=2020&kommun={municipality}&medlemsvkyrkan={churchMemeber}";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "inesssl=1191591305.20480.0000");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var taxTable = await Task.FromResult(JsonConvert.DeserializeObject<TaxTableResponse>(response.Content));
            return taxTable.TaxTable;
        }

        public async Task<int> GetTaxToDeductFromTaxAgency(int tablenr, string numberOfdays, int incomeTo, int incomeYear, int IncomeFrom)
        {
            var baseUrl = $"https://skatteverket.entryscape.net/rowstore/dataset/88320397-5c32-4c16-ae79-d36d95b17b95?tabellnr={tablenr}&antal dgr={numberOfdays}&inkomst t.o.m.={incomeTo}&år={incomeYear}&inkomst fr.o.m.={IncomeFrom}";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "inesssl=1191591305.20480.0000");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var taxToDeduct = await Task.FromResult(JsonConvert.DeserializeObject<DeductTaxResponse>(response.Content));
            return taxToDeduct.IncomeResults[0].ColumnOne;
        }
    }
}