using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.ViewModels.Components
{
    public class CategoryQuestionsViewModel : ViewModelBase
    {
        private ObservableCollection<QuestionModel> _questions;
        private readonly IQuestionService _questionService;
        private readonly int _categoryId;
        public ICommand QuestionSelectedCommand { get; }
        public ObservableCollection<QuestionModel> Questions
        {
            get => _questions;
            set
            {
                _questions = value;
                OnPropertyChanged(nameof(Questions));
            }
        }
        public CategoryQuestionsViewModel(IQuestionService questionService, int CategoryId)
        {
            _questionService = questionService;
            _categoryId = CategoryId;
            LoadQuestions();
            QuestionSelectedCommand = new QuestionSelectedCommand(QuestionSelected);
        }
        private async void LoadQuestions()
        {
            var questions = await _questionService.GetQuestionsByCategoryIdAsync(_categoryId);
            Questions = new ObservableCollection<QuestionModel>(questions);
        }
        private void QuestionSelected()
        {
            //TODO: 
        }
    }
}
