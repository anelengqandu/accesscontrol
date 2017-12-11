using System;
using SQLite;

namespace NMBM.Entities
{
    [Table("Burial")]
    public class Burial
    {

        [PrimaryKey, AutoIncrement, Unique]
        public long Id { get; set; }
        public long BurialApplication { get; set; }
        public long CemeterySupervisorId { get; set; }
        public string Undertaker { get; set; }
        public int AccessCounter { get; set; }
        public string Cemetery { get; set; }
        public long CemeteryId { get; set; }
        public DateTime? BurialDate { get; set; }
        public DateTime? OptDate { get; set; }
        public string DeceasedDetails { get; set; }
    }
}
