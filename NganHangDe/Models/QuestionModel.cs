using NganHangDe.Commands;
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


    }
}
