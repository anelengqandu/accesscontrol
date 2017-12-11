using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NMBM.Helpers;
using NMBM.Models;

namespace NMBM.Services.Burials
{
    internal class BurialService: IBurialService
    {
        public async Task<ReturnResult> GetBurialAsync(long id)
        {
            var hasError = false;
            var cemetery = new CemeteryModelInput
            {
                cemeteryId = id
            };
            var httpClient = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(cemetery), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Constants.BaseApiAddress + "/api/BurialsApi/Cemeteries", stringContent);
            var objBurials = JsonConvert.DeserializeObject<BurialModel>(
                JObject.Parse(response.Content.ReadAsStringAsync().Result)["data"].ToString()).CemeteryList;
            if (!response.IsSuccessStatusCode)
                hasError = true;
            return new ReturnResult { status = !hasError ? "Success" : "Fail", descripText = response.StatusCode.ToString(), objBurial = objBurials };
        }

        public async Task<ReturnResult> UpdateBurialCounterAsync(long burialApplication)
        {
            var hasError = false;
            var burialApp = new BurialModelAccessCounterInput
            {
                BurialApplication = burialApplication,
            };
            var httpClient = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(burialApp), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Constants.BaseApiAddress + "/api/BurialsApi/BurialApp", stringContent);

            if (!response.IsSuccessStatusCode)
                hasError = true;

            return new ReturnResult { status = !hasError ? "Success" : "Fail", descripText = response.StatusCode.ToString()};
        }

        public async Task<ReturnResult> BurialConfirmationAsync(long burialApplication)
        {
            var hasError = false;
            var burialApp = new BurialModelAccessCounterInput
            {
                BurialApplication = burialApplication,
            };
            var httpClient = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(burialApp), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Constants.BaseApiAddress + "/api/BurialsApi/OtpDate", stringContent);

            if (!response.IsSuccessStatusCode)
                hasError = true;

            return new ReturnResult { status = !hasError ? "Success" : "Fail", descripText = response.StatusCode.ToString() };
        }
    }
}



