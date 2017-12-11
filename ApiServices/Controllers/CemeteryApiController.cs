using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiServices.EntityFramework.Entities;
using ApiServices.Models;
using ApiServices.Utils;

namespace ApiServices.Controllers
{
    public class CemeteryApiController : ApiController
    {
        [System.Web.Http.Route("api/CemeteryApi/CemeteryById/")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPut]
        public DBResult CemeteryById(CemeteryGeolocation_Input model)
        {
            CemeteryGeolocationModel result = null;
            var hasError = false;
            var errorText = "";

            try
            {
                if (!ModelState.IsValid && model == null)
                    hasError = true;
                else
                    result = CEM_LIST_CEMETERY.GetCemeteryGeolocation(model.CemeteryId);
            }
            catch (Exception e)
            {
                hasError = true;
                errorText = e.Message;
            }
            return new DBResult { status = !hasError ? "Success" : "Fail", descripText = errorText, data = new CemeteryGeolocation {   CemeteryLocation= result } };
        }
    }
}
