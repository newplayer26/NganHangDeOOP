using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.ViewModels.StartupViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class RemoveQuestionsFromQuizCommand : AsyncCommandBase
    {

        private QuizService _service;
        private EditingQuizViewModel _viewModel;
        public RemoveQuestionsFromQuizCommand(EditingQuizViewModel viewModel)
        {
            _service = new QuizService();
            _viewModel = viewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                List<QuestionModel> selectedQuestions = _viewModel.QuestionList.Where(q => q.IsSelected == true).ToList();
                foreach (QuestionModel question in selectedQuestions)
                {
                    await _service.DeleteSingleQuestionFromQuizAsync(question.Id, _viewModel.QuizId);
                }
                await _viewModel.LoadQuiz();
            }
            catch (Exception)
            {

            }
        }
    }
}
