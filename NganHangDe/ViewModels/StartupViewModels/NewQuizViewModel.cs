using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NganHangDe.ViewModels.StartupViewModels
{
    public class NewQuizViewModel:ViewModelBase
    {
        public QuizModel Model = new QuizModel();

        public String Name { 
            get {
                return Model.Name;
            } 
            set { 
                Model.Name = value;
                
                OnPropertyChanged(nameof(CanCreateQuiz));
                Console.Write(CanCreateQuiz);
            }

        }
        public String Description
        {
            get
            {
                return Model.Description;
            }
            set
            {
                Model.Description = value;
                OnPropertyChanged(nameof(CanCreateQuiz));
            }
        }
        public bool CanCreateQuiz => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description);
        public ICommand CreateQuizCommand { get; }

        private readonly NavigationStore _ancestorNavigationStore;
        public ICommand ToAllQuizzesViewCommand { get;  }
        public NewQuizViewModel(NavigationStore ancestorNavigationStore)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            ToAllQuizzesViewCommand = new NavigateCommand<AllQuizzesViewModel>(_ancestorNavigationStore, typeof(AllQuizzesViewModel));
            CreateQuizCommand = new CreateQuizCommand(this);
        }
    }
}
