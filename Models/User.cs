namespace CheckListJob.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }

        public ICollection<ListLog> ListLogs { get; set; }

    }
}
