using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Connectivity.Plugin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NMBM.Helpers;
using NMBM.Models;

namespace NMBM.Services.Authentication
{
    internal class AuthService : IAuthService
    {
   
        public async Task<ReturnResult> LoginAsync(string userName, string password)
        {
            var hasError = false;
            var auth = new SigninModel
            {
                Username = userName,
                Password = password
            };

            var url = Constants.BaseApiAddress + "/api/AuthenticationApi/Login";
           var httpClient = new HttpClient();
           var stringContent = new StringContent(JsonConvert.SerializeObject(auth), Encoding.UTF8, "application/json");
           var response = await httpClient.PostAsync(url, stringContent);
           var objSupervisor = JsonConvert.DeserializeObject<SupervisorModel>(
               JObject.Parse(response.Content.ReadAsStringAsync().Result)["data"].ToString());
           if (!response.IsSuccessStatusCode)
               hasError = true;

           return new ReturnResult { status = !hasError ? "Success" : "Fail", descripText = response.StatusCode.ToString(), objSupervisor = objSupervisor };

        }
    }
}
