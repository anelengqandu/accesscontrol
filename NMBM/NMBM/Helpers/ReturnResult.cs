

using System;
using System.Collections.Generic;
using NMBM.Models;

namespace NMBM.Helpers
{
    public class ReturnResult
    {
        public bool success = true;
        public string descripText;
        public string status;
        public List<BurialModel> objBurial;
        public SupervisorModel objSupervisor;
        public DeviceModel objDevice;

        public ReturnResult() { }
    }
}
