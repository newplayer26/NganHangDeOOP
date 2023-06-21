using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.ModelsDb;
using NganHangDe.Services;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.ViewModels.StartupViewModels
{
    
    public class AddFromQuestionBankViewModel : ViewModelBase
    {
        private readonly NavigationStore _ancestorNavigationStore;
        private int _id;
        public RelayCommand ToEditingQuizViewCommand { get; private set; }
        public AddFromQuestionBankViewModel(NavigationStore ancestorNavigationStore, int id)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            _id = id;
            ToEditingQuizViewCommand = new RelayCommand(ExecuteToEditingQuizViewCommand);
        }
        private void ExecuteToEditingQuizViewCommand(object parameter)
        {
            int id = _id;
            EditingQuizViewModel editingQuizViewModel = new EditingQuizViewModel(_ancestorNavigationStore, id);
            _ancestorNavigationStore.CurrentViewModel = editingQuizViewModel;
        }

    }
}
