using NganHangDe.Commands;
using NganHangDe.ModelsDb;
using NganHangDe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.Models
{
    public class QuestionModel : ViewModelBase
    {
        private string _name;
        private string _text;
        private List<AnswerModel> _answers;
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public List<AnswerModel> Answers
        {
            get => _answers;
            set
            {
                _answers = value;
                OnPropertyChanged(nameof(Answers));
            }
        }
    }
}
