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
    public class QuizPageViewModel : ViewModelBase
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
        public int Id
        {
            get
            {
                return _model.Id;
            }
        }
        private bool _isPopupVisible;
        public bool IsPopupVisible
        {
            get { return _isPopupVisible; }
            set
            {
                _isPopupVisible = value;
                OnPropertyChanged(nameof(IsPopupVisible));
            }
        }
        public RelayCommand ShowPopupCommand { get; private set; }
        public RelayCommand ToEditingQuizViewCommand { get; private set; }
        public RelayCommand ToPreviewQuizViewCommand { get; private set; }
        public QuizPageViewModel(NavigationStore ancestorNavigationStore, QuizModel model)
        {
            _model = model;
            _ancestorNavigationStore = ancestorNavigationStore;
            //ToEditingQuizViewCommand = new NavigateCommand<EditingQuizViewModel>(_ancestorNavigationStore, typeof(EditingQuizViewModel));
            ToEditingQuizViewCommand = new RelayCommand(ExecuteToEditingQuizViewCommand);
            ShowPopupCommand = new RelayCommand(ExecuteShowPopupCommand);
            ToPreviewQuizViewCommand = new RelayCommand(ExecuteToPreviewQuizViewCommand);
        }
        //public ICommand ToEditingQuizViewCommand { get; }
        private void ExecuteToEditingQuizViewCommand(object parameter)
        {
            int quizId = _model.Id;
            Console.WriteLine(quizId);
            EditingQuizViewModel editingQuizViewModel = new EditingQuizViewModel(_ancestorNavigationStore, quizId);
            _ancestorNavigationStore.CurrentViewModel = editingQuizViewModel;
        }
        private void ExecuteShowPopupCommand(object parameter)
        {
            IsPopupVisible = true;
        }
        private void ExecuteToPreviewQuizViewCommand(object parameter)
        {
            int quizId= _model.Id;
            Console.WriteLine(quizId);
            PreviewQuizViewModel previewQuizViewModel = new PreviewQuizViewModel(_ancestorNavigationStore, quizId);
            _ancestorNavigationStore.CurrentViewModel = previewQuizViewModel;
        }
    }
}