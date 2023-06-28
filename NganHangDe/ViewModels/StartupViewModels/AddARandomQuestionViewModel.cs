using NganHangDe.Commands;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.ViewModels.StartupViewModels
{
    class AddARandomQuestionViewModel : ViewModelBase
    {
        private readonly NavigationStore _ancestorNavigationStore;
        private int _quizId;
        public RelayCommand ToEditingQuizViewCommand { get; private set; }
        public AddARandomQuestionViewModel(NavigationStore ancestorNavigationStore, int quizId)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            _quizId = quizId;
            ToEditingQuizViewCommand = new RelayCommand(ExecuteToEditingQuizViewCommand);
        }
        private void ExecuteToEditingQuizViewCommand(object parameter)
        {
            int quizId = _quizId;
            EditingQuizViewModel editingQuizViewModel = new EditingQuizViewModel(_ancestorNavigationStore, quizId);
            _ancestorNavigationStore.CurrentViewModel = editingQuizViewModel;
        }
    }
}
