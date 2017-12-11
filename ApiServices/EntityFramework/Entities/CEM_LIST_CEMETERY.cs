using System.Linq;
using ApiServices.EntityFramework.EntityModel;
using ApiServices.Models;

namespace ApiServices.EntityFramework.Entities
{
    public class CEM_LIST_CEMETERY
    {

        public static CemeteryGeolocationModel GetCemeteryGeolocation(long cemeteryId)
        {
            var q = from row in DataAccess.Context.CEM_LIST_CEMETERY
                    where row.ID == cemeteryId
                    select new CemeteryGeolocationModel
                    {
                        Latitude = row.LATTITUDE,
                        Longitude = row.LONGITUDE,
                    };
        
            var clientTeam = q.FirstOrDefault();
            return clientTeam;
        }
    }
}