namespace CheckListJob.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string Tittle { get; set; }

        public ICollection<ListShift>? Lists { get; set; }

    }
}
