
using System;
using System.Threading.Tasks;
using System.Web.Http;
using ApiServices.EntityFramework.Authentication;
using ApiServices.Models;
using ApiServices.Utils;

namespace ApiServices.Controllers
{
    public class AuthenticationApiController : ApiController
    {
        [System.Web.Http.Route("api/AuthenticationApi/Login/")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPut]
        public async Task<DBResult> Login(AuthenticationModel model)
        {
            SupervisorModel result = null;
            bool hasError = false;
            string errorText = "";

            try
            {
                if (ModelState.IsValid || model != null)
                {
                    result = await Authentication.AuthenticateUser(model.Username, model.Password);
                    if (result == null)
                    {
                        return new DBResult
                        {
                            status = !hasError ? "Success" : "Fail",
                            descripText = "Username/Password is not valid",
                            data = null
                        };
                    }
                }
                else
                {
                    hasError = true;
                }

            }
            catch (Exception e)
            {
                hasError = true;

                return new DBResult
                {
                    status = !hasError ? "Success" : "Fail",
                    descripText = e.Message.ToString(),
                    data = null
                };
            }
            return new DBResult { status = !hasError ? "Success" : "Fail", descripText = errorText, data = new SupervisorModel { SupervisorId = result.SupervisorId, Name = result.Name, Surname = result.Surname, CemeteryId = result.CemeteryId, UserName = result.UserName, SupervisorPassword = result.SupervisorPassword } };
        }

    }
}
