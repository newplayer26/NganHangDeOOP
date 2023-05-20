using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Stores;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.ViewModels.StartupViewModels
{
    public class AllQuizzesViewModel:ViewModelBase
    {
        private readonly NavigationStore _ancestorNavigationStore;
        private List<QuizModel> _quizList;
        public IEnumerable<QuizModel> QuizList => _quizList;
        public ICommand LoadQuizzesCommand { get; set; }
        public ICommand ToQuizPageCommand { get;  }
        public AllQuizzesViewModel(NavigationStore ancestorNavigationStore)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            LoadQuizzesCommand = new LoadQuizzesCommand(LoadQuizzes);
            LoadQuizzesCommand.Execute(null);
            ToQuizPageCommand = new NavigateCommand<QuizPageViewModel>(_ancestorNavigationStore, typeof(QuizPageViewModel));
        }

        public void LoadQuizzes(List<QuizModel> list)
        {

            _quizList = list;
            OnPropertyChanged(nameof(QuizList));

        } 
    }
   
}
