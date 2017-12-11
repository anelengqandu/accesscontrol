using System.Collections.Generic;
using System.Linq;
using ApiServices.EntityFramework.EntityModel;
using ApiServices.Models;

namespace ApiServices.EntityFramework.Entities
{
    public class CemBurialCemeteryUndertaker
    {
        public static List<BurialModel> GetBurialList(int cemeteryId)
        {
            var q = from cemetery in DataAccess.Context.AccessControlListByCemetery(cemeteryId)
                select new
                {
                    cemetery.BurialApplication,
                    cemetery.Undertaker,
                    cemetery.Cemetery,
                    cemetery.BurialDate,
                    cemetery.AccessCount,
                    cemetery.OTPDate,
                    cemetery.DeceasedDetails
                };
            var cemeteryList = q.ToList();

            return cemeteryList.Select(cemetery => new BurialModel
            {
                BurialApplication = cemetery.BurialApplication,
                Undertaker = cemetery.Undertaker,
                Cemetery = cemetery.Cemetery,
                BurialDate = cemetery.BurialDate,
                AccessCount = cemetery.AccessCount,
                OptDate = cemetery.OTPDate,
                DeceasedDetails = cemetery.DeceasedDetails
            }).ToList();

        }

    }
}