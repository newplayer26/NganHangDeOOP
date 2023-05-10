using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.ModelsDb
{
    public class Quiz
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        public TimeSpan TimeLimit { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
