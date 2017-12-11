using System;
using System.Collections.Generic;


namespace ApiServices.Models
{
    public class BurialModel
    {
        public List<BurialModel> CemeteryList { get; set; }
        public long BurialApplication { get; set; }
        public string Undertaker { get; set; }
        public string Cemetery { get; set; }
        public DateTime? BurialDate { get; set; }
        public string BurialDateS => BurialDate?.ToString("dd MMM yyyy") ?? "Pending";
        public int AccessCount { get; set; }
        public DateTime? OptDate { get; set; }
        public string OptDateS => OptDate?.ToString("dd MMM yyyy") ?? "Pending";
        public string DeceasedDetails { get; set; }
    }
    public class BurialModelInput
    {
     
        public int CemeteryId { get; set; }
     
    }

    public class BurialModelAccessCounterInput
    {
        public long BurialApplication { get; set; }
        public int AccessCounter { get; set; }
        public int? CemeterySupervisorId { get; set; }

    }


    public class BurialModelAccessCounterOutput
    {
      
        public long? AccessCounter { get; set; }
        public DateTime? OtpDate { get; set; }
        public string OtpDateS => OtpDate?.ToString("dd MMM yyyy") ?? "Pending";
    }



    public class BurialModelOptDateInput
    {

        public DateTime OTPDate { get; set; }

    }

}