using NganHangDe.Commands;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace NganHangDe.ViewModels.TabbedNavigationTabViewModels
{
    public class QuestionsTabViewModel : ViewModelBase
    {
        public ICommand ToNewQuestionViewCommand { get; }
        public QuestionsTabViewModel(NavigationStore ancestorNavigationStore)
        {
            ToNewQuestionViewCommand = new NavigateCommand<NewQuestionViewModel>(ancestorNavigationStore,typeof(NewQuestionViewModel));    
        }
    }
}
