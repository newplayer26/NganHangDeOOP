using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Models
{
    public class Answer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Text { get; set; }
        public double Grade { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }

    }
}
