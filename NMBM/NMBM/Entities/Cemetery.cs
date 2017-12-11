using SQLite;

namespace NMBM.Entities
{
    [Table("Cemetery")]
    public class Cemetery
    {

        [PrimaryKey, AutoIncrement, Unique]
        public long Id { get; set; }
        public long CemeteryId { get; set; }
        public string Description { get; set; }
        public double? Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}
