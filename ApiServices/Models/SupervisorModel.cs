using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiServices.Models
{
    public class SupervisorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long? SupervisorId { get; set; }
        public long? CemeteryId { get; set; }
        public string Fullname => Name + " " + Surname;
        public string UserName { get; set; }
        public string SupervisorPassword { get; set; }
    }
}