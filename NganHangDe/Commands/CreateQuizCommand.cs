using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.ViewModels.StartupViewModels;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class CreateQuizCommand:AsyncCommandBase
    {
        private NewQuizViewModel _viewModel;
        private QuizService _service = new QuizService();
        public CreateQuizCommand(NewQuizViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                
               _viewModel.ToAllQuizzesViewCommand.Execute(null);
            }
            catch (Exception)
            {

            }
        }
        public override bool CanExecute(object parameter)
        {
            return _viewModel.CanCreateQuiz && base.CanExecute(parameter);
        }
        public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NewQuizViewModel.CanCreateQuiz))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
