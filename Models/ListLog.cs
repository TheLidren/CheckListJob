﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CheckListJob.Models
{
    public class ListLog
    {
        public int Id { get; set; }
        public int? ShiftTaskId { get; set; }

        //[ForeignKey("ShiftTaskId")]
        public ShiftTask ShiftTask { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public DateTime MarkAction { get; set; }

    }
}
