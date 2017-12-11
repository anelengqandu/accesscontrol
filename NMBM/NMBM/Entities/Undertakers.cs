using SQLite;

namespace NMBM.Entities
{
    [Table("Undertaker")]
    public class Undertakers
    {
        [PrimaryKey, AutoIncrement, Unique]
        public long Id { get; set; }
        public long UndertakerId { get; set; }
        public string Description { get; set; }

    }
}
