using System.ComponentModel.DataAnnotations;

namespace CheckListJob.Models
{
    public class ListShift
    {
        public Int16 Id { get; set; }

        [Required(ErrorMessage = "Данное поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Введите корректно размерность", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Данное поле не должно быть пустым")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Данное поле не должно быть пустым")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "Данное поле не должно быть пустым")]
        public TimeSpan FinishTime { get; set; }

        [Required(ErrorMessage = "Данное поле не должно быть пустым")]
        public int ShiftId { get; set; }
        public Shift? Shift { get; set; }
        public bool Status { get; set; }

        public ICollection<ListLog>? ListLogs { get; set; }

    }

}
