namespace CheckListJob.Models
{
    public class ListLog
    {
        public int Id { get; set; }
        public int? ListId { get; set; }

        public ListShift ListShift { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public DateTime MarkAction { get; set; }

    }
}
