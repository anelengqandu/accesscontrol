using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMBM.Models
{
    public class DeviceModel
    {
        public CemeteryGeolocationModel CemeteryLocation { get; set; }

    }



    public class DeviceGeolocationModel
    {
        public long DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class CemeteryGeolocationModel
    {
        public string CemeteryName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
