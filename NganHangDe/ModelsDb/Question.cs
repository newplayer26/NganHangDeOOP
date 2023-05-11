using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.ModelsDb
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
