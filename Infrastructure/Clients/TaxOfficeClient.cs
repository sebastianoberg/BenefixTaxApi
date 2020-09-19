using System;
using System.Threading.Tasks;
using BenefitTaxApi.Infrastructure.Interfaces;
using BenefitTaxApi.Models.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace BenefitTaxApi.Infrastructure.Clients
{
    public class TaxOfficeClient : ITaxOfficeClient
    {
        public TaxOfficeClient()
        {
        }

        public async Task<TaxTableResponse> GetTaxTableFromTaxOffice(string Congregation, bool ChurchMemeber)
        {
            return await GetTaxTable(Congregation, ChurchMemeber);
        }

        public async Task<DeductTaxResponse> GetFromTaxOffice(string tablenr, string numberOfdays, int incomeTo, int incomeYear, int IncomeFrom)
        {
            return await GetTaxToDeduct(tablenr, numberOfdays, incomeTo, incomeYear, IncomeFrom);
        }

        public async Task<TaxTableResponse> GetTaxTable(string Congregation, bool ChurchMemeber)
        {
            var congregation = Congregation;
            var churchMemeber = ChurchMemeber;
            var baseUrl = $"https://www.skatteverket.se/st-api/rest/v1/skattetabell?forsamling=&inkomstar=2020&kommun={congregation}&medlemsvkyrkan={churchMemeber}";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "inesssl=1191591305.20480.0000");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return await Task.FromResult(JsonConvert.DeserializeObject<TaxTableResponse>(response.Content));
        }

        public async Task<DeductTaxResponse> GetTaxToDeduct(string tablenr, string numberOfdays, int incomeTo, int incomeYear, int IncomeFrom)
        {
            var baseUrl = $"https://skatteverket.entryscape.net/rowstore/dataset/88320397-5c32-4c16-ae79-d36d95b17b95?tabellnr={tablenr}&antal dgr={numberOfdays}&inkomst t.o.m.={incomeTo}&år={incomeYear}&inkomst fr.o.m.={IncomeFrom}";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "inesssl=1191591305.20480.0000");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return await Task.FromResult(JsonConvert.DeserializeObject<DeductTaxResponse>(response.Content));
        }
    }
}
