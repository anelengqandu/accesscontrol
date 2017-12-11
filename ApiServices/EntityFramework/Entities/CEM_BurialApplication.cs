using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiServices.EntityFramework.EntityModel;
using ApiServices.Models;
using ApiServices.Utils;

namespace ApiServices.EntityFramework.Entities
{
    public class CEM_BurialApplication
    {
        public static DBResult addAccessCounter(BurialModelAccessCounterInput input)
        {
            var hasError = false;
            var errorText = "";
            try
            {
                var updateBurialApp = DataAccess.Context.CEM_BurialApplication.Find(input.BurialApplication);
                if (updateBurialApp == null)
                {
                    return new DBResult
                    {
                        status = "Fail",
                        descripText = "Record not found!",
                        success = false
                    };
                }
                else
                {
                    updateBurialApp.AccessCount = (updateBurialApp.AccessCount ?? 0);
                    updateBurialApp.AccessCount = updateBurialApp.AccessCount + 1;// input.AccessCounter;
                    updateBurialApp.CemeterySupervisorId = input.CemeterySupervisorId;

                    DataAccess.Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                hasError = true;
                errorText = ex.Message;
                return new DBResult { status = !hasError ? "Success" : "Fail", descripText = errorText };
            }

            return new DBResult { status = !hasError ? "Success" : "Fail", descripText = errorText, data = new BurialModelAccessCounterOutput { AccessCounter = input.AccessCounter } };
        }



        public static DBResult editAccessOtpDate(long BurialApplication)
        {
            bool hasError = false;
            string errorText = "";

            try
            {
                var ediBurialApp = DataAccess.Context.CEM_BurialApplication.Find(BurialApplication);
                if (ediBurialApp == null)
                {
                    hasError = true;
                    return new DBResult
                    {
                        status = !hasError ? "Success" : "Fail",
                        descripText = "Record not found!",
                        success = false
                    };
                }
                else
                {
                    ediBurialApp.OTPDate = DateTime.Now;
                    DataAccess.Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                hasError = true;
                errorText = ex.Message;
                return new DBResult { status = !hasError ? "Success" : "Fail", descripText = errorText };
            }

            return new DBResult { status = !hasError ? "Success" : "Fail", descripText = errorText, data = null };
        }

        public static BurialModelAccessCounterOutput GetAccessCounter(long BurialApplicationId)
        {
            var q = from row in DataAccess.Context.CEM_BurialApplication
                    where row.app_ID == BurialApplicationId
                    select new BurialModelAccessCounterOutput
                    {

                        AccessCounter = row.AccessCount,
                        OtpDate = row.OTPDate

                    };

            var counter = q.FirstOrDefault();
            return counter;
        }
    }
}