using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NganHangDe.ViewModels;

namespace NganHangDe.Models
{
    public class QuizModel : ViewModelBase
    {
        private string _name;
        public int Id { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private String _description;

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

    }
}
