using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NMBM.Helpers;
using NMBM.Models;

namespace NMBM.Services.Cemetery
{
    internal class CemeteryService: ICemeteryService
    {
        public async Task<ReturnResult> GetCemeteryAsync(long id)
        {
            var hasError = false;
            var cemetery = new CemeteryModelInput
            {
                cemeteryId = id
            };

            var uri = Constants.BaseApiAddress + "/api/CemeteryApi/CemeteryById";
            var httpClient = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(cemetery), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, stringContent);

            var objDevice = JsonConvert.DeserializeObject<DeviceModel>(
                                      JObject.Parse(response.Content.ReadAsStringAsync().Result)["data"].ToString());

            if (!response.IsSuccessStatusCode)
                hasError = true;

            return new ReturnResult { status = !hasError ? "Success" : "Fail", descripText = response.StatusCode.ToString(), objDevice = objDevice };

        }
    }
}
