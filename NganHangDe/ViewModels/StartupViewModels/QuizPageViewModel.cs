using NganHangDe.Models;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.ViewModels.StartupViewModels
{
    public class QuizPageViewModel:ViewModelBase
    {
        private QuizModel _model;
        public String Name
        {
            get
            {
                return _model.Name; 
            }
        }

        public QuizPageViewModel(NavigationStore ancestorNavigationStore, QuizModel model)
        {
            _model = model;
        }
    }
}
