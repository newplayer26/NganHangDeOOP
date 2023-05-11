using NganHangDe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Models
{
    public class AnswerModel : ViewModelBase
    {
        private double _grade;
        private string _text;
        public int Id { get; set; }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public double Grade
        {
            get => _grade;
            set
            {
                _grade = value;
                OnPropertyChanged(nameof(Grade));
            }
        }
    }
}
