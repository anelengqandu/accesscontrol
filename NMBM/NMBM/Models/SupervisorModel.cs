namespace NMBM.Models
{
    public class SupervisorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long SupervisorId { get; set; }
        public long CemeteryId { get; set; }
        public string Fullname => Name + " " + Surname;
        public string UserName { get; set; }
        public string SupervisorPassword { get; set; }

    }
}
