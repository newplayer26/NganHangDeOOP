using Microsoft.Identity.Client;
using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.ModelsDb;
using NganHangDe.Services;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.ViewModels.QuizUIViewModels
{
    public class PreviewQuizViewModel : ViewModelBase
    {
        private readonly NavigationStore _ancestorNavigationStore;
        private int _quizId;
        private QuizService _quizService;
        private QuestionService _questionService;
        private ObservableCollection<QuestionModel> _questionList = new ObservableCollection<QuestionModel>();
        public ObservableCollection<QuestionModel> QuestionList => _questionList;
        private ObservableCollection<QuestionModel> _loadedQuestionList = new ObservableCollection<QuestionModel>();
        public ObservableCollection<QuestionModel> LoadedQuestionList => _loadedQuestionList;
        public PreviewQuizViewModel(NavigationStore ancestorNavigationStore, int quizId)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            _quizId = quizId;
            _quizService = new QuizService();
            _questionService = new QuestionService();
            _ = LoadQuestionsAsync();
        }
        private async Task LoadQuestionsAsync()
        {
            try
            {
                var questions = await _quizService.GetAllQuestionsFromQuizAsync(_quizId);

                _questionList.Clear();
                foreach (var question in questions)
                {
                    var questionModel = new QuestionModel
                    {
                        Id = question.Id,
                        Text = question.Text,
                    };

                    var loadQuestionCommand = new LoadSingleQuestionCommand(LoadQuestionCallback);
                    await loadQuestionCommand.ExecuteAsync(questionModel.Id);

                    _questionList.Add(questionModel);
                }

                Console.WriteLine(_questionList.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }
        private void LoadQuestionCallback(QuestionModel question, List<AnswerModel> answers)
        {
            question.Answers = answers;
            _loadedQuestionList.Add(question);
        }
    }
}

