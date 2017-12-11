using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiServices.Models
{
    public class CemeteryGeolocationModel
    {
    
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
    public class CemeteryGeolocation
    {

        public CemeteryGeolocationModel CemeteryLocation { get; set; }
        
    }

    public class CemeteryGeolocation_Input
    {

        public int CemeteryId { get; set; }

    }
}