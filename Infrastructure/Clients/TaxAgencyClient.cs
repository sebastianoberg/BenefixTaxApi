using System.Collections.Generic;
using System.Threading.Tasks;
using BenefitTaxApi.API.Contracts;
using BenefitTaxApi.Infrastructure.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace BenefitTaxApi.Infrastructure.Clients
{
    public class TaxAgencyClient : ITaxAgencyClient
    {
        public async Task<int> GetTaxTable(string municipality)
        {
            var churchMemeber = false;
            return await GetTaxTableFromTaxOffice(municipality, churchMemeber);
        }

        public async Task<int> GetChurchMemberTaxTable(string municipality, bool churchMemeber, string congregation)
        {
            return await GetChurchMemberTaxTableFromTaxOffice(municipality, churchMemeber, congregation);
        }

        public async Task<int> GetTaxToDeduct(int tablenr, string numberOfdays, int incomeTo, int incomeYear, int IncomeFrom)
        {
            return await GetTaxToDeductFromTaxAgency(tablenr, numberOfdays, incomeTo, incomeYear, IncomeFrom);
        }

        public async Task<MunicipalitiesResponse> GetAllMunicipalities()
        {
            return await GetMunicipalities();
        }

        public async Task<List<string>> GetAllCongregations(int incomeYear, string municipality)
        {
            return await GetCongregations(incomeYear, municipality);
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

        public async Task<int> GetChurchMemberTaxTableFromTaxOffice(string municipality, bool churchMemeber, string congregation)
        {
            var baseUrl = $"https://www.skatteverket.se/st-api/rest/v1/skattetabell?forsamling={congregation}&inkomstar=2020&kommun={municipality}&medlemsvkyrkan={churchMemeber}";

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
            var baseUrl = $"https://skatteverket.entryscape.net/rowstore/dataset/88320397-5c32-4c16-ae79-d36d95b17b95?tabellnr={tablenr}&antal%20dgr={numberOfdays}&inkomst%20t.o.m.={incomeTo}&%C3%A5r={incomeYear}&inkomst%20fr.o.m.={IncomeFrom}&_limit=100&_offset=0";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "inesssl=1191591305.20480.0000");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var taxToDeduct = await Task.FromResult(JsonConvert.DeserializeObject<DeductTaxResponse>(response.Content));
            return taxToDeduct.Results[0].ColumnOne;
        }

        public async Task<MunicipalitiesResponse> GetMunicipalities()
        {
            var baseUrl = $"https://www.skatteverket.se/st-api/rest/v1/kommuner";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "inesssl=1191591305.20480.0000");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var municipalities = await Task.FromResult(JsonConvert.DeserializeObject<MunicipalitiesResponse>(response.Content));

            return municipalities;
        }

        public async Task<List<string>> GetCongregations(int incomeYear, string municipality)
        {
            var baseUrl = $"https://www.skatteverket.se/st-api/rest/v1/forsamlingar?inkomstar={incomeYear}&kommun={municipality}";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "inesssl=1191591305.20480.0000");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var congregations = await Task.FromResult(JsonConvert.DeserializeObject<List<string>>(response.Content));
            return congregations;
        }
    }
}
