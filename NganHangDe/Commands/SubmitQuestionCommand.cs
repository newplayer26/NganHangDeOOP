using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class SubmitQuestionCommand:AsyncCommandBase
    {   
        private NewQuestionViewModel _viewModel;
        private QuestionService _service = new QuestionService();
        public SubmitQuestionCommand(NewQuestionViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
            
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
               QuestionModel question = _viewModel.Question;
               int categoryId = _viewModel.SelectedCategory.Id;
               List<AnswerModel> answers = _viewModel.ValidatedAnswers.ToList();
               await _service.CreateQuestionAsync(question, categoryId, answers);
                if (parameter is Action action && action.Method.ReturnType == typeof(void))
                {
                    action();
                }
            }
            catch (Exception)
            {

            }
        }
        public override bool CanExecute(object parameter)
        {
            return _viewModel.CanCreateQuestion && base.CanExecute(parameter);
        }
        public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NewQuestionViewModel.CanCreateQuestion))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
