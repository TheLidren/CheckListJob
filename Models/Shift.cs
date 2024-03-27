namespace CheckListJob.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ShiftTask>? ShiftTasks { get; set; }

    }
}
