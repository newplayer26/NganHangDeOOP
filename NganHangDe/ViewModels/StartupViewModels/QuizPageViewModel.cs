using Microsoft.Identity.Client;
using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.ViewModels.StartupViewModels
{
    public class QuizPageViewModel:ViewModelBase
    {
        private readonly NavigationStore _ancestorNavigationStore;
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
            _ancestorNavigationStore = ancestorNavigationStore;
            ToEditingQuizCommand = new NavigateCommand<EditingQuizViewModel>(_ancestorNavigationStore, typeof(EditingQuizViewModel));

        }
        public ICommand ToEditingQuizCommand { get; }
    }
}
