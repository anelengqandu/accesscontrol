using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NMBM.Models
{
   public class BurialModel
    {
        public List<BurialModel> CemeteryList { get; set; }
        private readonly Random _random = new Random();
        public long BurialApplication { get; set; }
        public string Undertaker { get; set; }
        public string SupervisorName { get; set; }
        public string Cemetery { get; set; }
        public Color Colors => GetRandomColor();
        private Color GetRandomColor()
        {
            return Color.FromRgb(_random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255));
        }
        public DateTime? BurialDate { get; set; }
        public string BurialDateS => BurialDate?.ToString("dd MMM yyyy") ?? "Pending";
        public int AccessCount { get; set; }
        public DateTime? OptDate { get; set; }
        public string OptDateS => OptDate?.ToString("dd MMM yyyy") ?? "Pending";
        public string DeceasedDetails { get; set; }
    }

    //public class Burial
    //{
    //    public BurialModelOutPut BurialApplication { get; set; }
    //    public SupervisorModel Supervisor { get; set; }

    //}

    public class BurialModelOutPut
    {

       
        public long BurialApplication { get; set; }
        public string Undertaker { get; set; }
        public long SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Cemetery { get; set; }
        public long CemeteryId { get; set; }
        public DateTime? BurialDate { get; set; }
        public string BurialDateS => BurialDate?.ToString("dd MMM yyyy") ?? "Pending";
        public int AccessCount { get; set; }
        public DateTime? OptDate { get; set; }
        public string OptDateS => OptDate?.ToString("dd MMM yyyy") ?? "Pending";
        public string DeceasedDetails { get; set; }

    }

    public class BurialModelInput
    {
        public long CemeteryId { get; set; }
    }

    public class BurialModelAccessCounterInput
    {
        public long BurialApplication { get; set; }
        public int AccessCounter { get; set; }
        public int? CemeterySupervisorId { get; set; }

    }

    public class BurialModelAccessCounterOutput
    {
        public int AccessCounter { get; set; }
        public DateTime? OtpDate { get; set; }
        public string OtpDateS => OtpDate?.ToString("dd MMM yyyy") ?? "Pending";
        public string DeceasedDetails { get; set; }
    }
}
