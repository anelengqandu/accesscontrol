using System;
using System.Collections.Generic;
using System.Web.Http;
using ApiServices.EntityFramework.Entities;
using ApiServices.Models;
using ApiServices.Utils;

namespace ApiServices.Controllers
{
    public class BurialsApiController : ApiController
    {
     
        [System.Web.Http.Route("api/BurialsApi/Cemeteries/")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPut]
        public DBResult Cemeteries(BurialModelInput model)
        {
            List<BurialModel> result = null;
            var hasError = false;
            var  errorText = "";

            try
            {
                if (!ModelState.IsValid && model == null)
                    hasError = true;
                else
                    result = CemBurialCemeteryUndertaker.GetBurialList(model.CemeteryId);
            }
            catch (Exception e)
            {
                hasError = true;
                errorText = e.Message;
            }
            return new DBResult { status = !hasError ? "Success" : "Fail", descripText = errorText, data = new BurialModel { CemeteryList = result} };
        }


        [System.Web.Http.Route("api/BurialsApi/BurialApp/")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPut]
        public DBResult BurialApp(BurialModelAccessCounterInput model)
        {
            DBResult result = null;
            var hasError = false;
            var errorText = "";

            try
            {
                if (!ModelState.IsValid && model == null)
                    hasError = true;
                else
                    result = CEM_BurialApplication.addAccessCounter(model);
            }
            catch (Exception e)
            {
                hasError = true;
                errorText = e.Message;
                return new DBResult { status = !hasError ? "Success" : "Fail", descripText = errorText, data = result.data };
            }
            return new DBResult { status = !hasError ? "Success" : "Fail", descripText = errorText, data = result.data};
           
        }



        [System.Web.Http.Route("api/BurialsApi/GetAccessCounter/")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPut]
        public DBResult GetAccessCounter(BurialModelAccessCounterInput model)
        {
            BurialModelAccessCounterOutput result = null;
            var hasError = false;
            var errorText = "";

            try
            {
                if (!ModelState.IsValid && model == null)
                    hasError = true;
                else
                    result = CEM_BurialApplication.GetAccessCounter(model.BurialApplication);
            }
            catch (Exception e)
            {
                hasError = true;
                errorText = e.Message;
            }
            return new DBResult { status = !hasError ? "Success" : "Fail", descripText = errorText, data = new BurialModelAccessCounterOutput { AccessCounter = result.AccessCounter, OtpDate = result.OtpDate} };
        }


        [System.Web.Http.Route("api/BurialsApi/OtpDate/")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPut]
        public DBResult OtpDate(BurialModelAccessCounterInput model)
        {
            DBResult result = null;
            var hasError = false;
            var errorText = "";

            try
            {
                if (!ModelState.IsValid && model == null)
                    hasError = true;
                else
                    result = CEM_BurialApplication.editAccessOtpDate(model.BurialApplication);
            }
            catch (Exception e)
            {
                hasError = true;
                errorText = e.Message;
            }
            return new DBResult { status = !hasError ? "Success" : "Fail", descripText = errorText, data = result.data };

        }



    }

}

