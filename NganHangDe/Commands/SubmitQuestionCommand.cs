using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NganHangDe.ModelsDb;
using System.Collections.ObjectModel;

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
               Question returnedModel = _viewModel.IsEditingQuestion? await _service.EditQuestionAsync(question, categoryId, answers) : await _service.CreateQuestionAsync(question, categoryId, answers);
                if (parameter is Action redirectAction)
                {
                    redirectAction();
                }else if(parameter is Action<QuestionModel, List<AnswerModel>> stayAction){
                    QuestionModel model = new QuestionModel
                    {
                        Id = returnedModel.Id,
                        Name = returnedModel.Name,
                        Text = returnedModel.Text,
                    };
                    List<AnswerModel> answerList = new List<AnswerModel>();
                    foreach(Answer answer in returnedModel.Answers) {
                        answerList.Add(new AnswerModel
                        {
                            Text = answer.Text,
                            Id = answer.Id,
                            Grade = answer.Grade,
                        });
                    };
                    stayAction(model, answerList);
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
