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
        private int _questionNumber = 1;
        //private ObservableCollection<QuestionModel> _questionList = new ObservableCollection<QuestionModel>();
        //public ObservableCollection<QuestionModel> QuestionList => _questionList;
        private ObservableCollection<QuestionModel> _loadedQuestionList = new ObservableCollection<QuestionModel>();
        public ObservableCollection<QuestionModel> LoadedQuestionList => _loadedQuestionList;
        private ObservableCollection<QuestionModel> _shuffledQuestionList;
        public ObservableCollection<QuestionModel> ShuffledQuestionList
        {
            get { return _shuffledQuestionList; }
            set
            {
                _shuffledQuestionList = value;
                OnPropertyChanged(nameof(ShuffledQuestionList));             
            }
        }
        private bool _isShuffleChecked;
        public bool IsShuffleChecked
        {
            get { return _isShuffleChecked; }
            set
            {
                _isShuffleChecked = value;
                OnPropertyChanged(nameof(IsShuffleChecked));
            }
        }
        public PreviewQuizViewModel(NavigationStore ancestorNavigationStore, int quizId, bool isShuffleChecked, ObservableCollection<QuestionModel> shuffledQuestionList)
        {
            _isShuffleChecked = isShuffleChecked;
            _shuffledQuestionList = shuffledQuestionList;
            _ancestorNavigationStore = ancestorNavigationStore;
            _quizId = quizId;
            _quizService = new QuizService();
            _ = LoadQuestionsAsync();
            
        }
        private async Task LoadQuestionsAsync()
        {
            if (_isShuffleChecked == false)
            {
                try
                {
                    var questions = await _quizService.GetAllQuestionsFromQuizAsync(_quizId);
                    foreach (var question in questions)
                    {

                        var questionModel = new QuestionModel
                        {
                            Id = question.Id,
                            Text = question.Text,
                            
                        };
                        var loadQuestionCommand = new LoadSingleQuestionCommand(LoadQuestionCallback);
                        
                        await loadQuestionCommand.ExecuteAsync(questionModel.Id);
                        Console.WriteLine(questionModel.QuestionNumber);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            }
            else
            {
                SetShuffledQuestionList(_shuffledQuestionList);
            }
        }
        private void LoadQuestionCallback(QuestionModel question, List<AnswerModel> answers)
        {
            
            question.Answers = answers;
            Console.WriteLine(question.IsMultipleAnswers);
            _loadedQuestionList.Add(question);
            question.QuestionNumber = _questionNumber++;
            //Console.WriteLine("PreviewQuizShuffle = " + IsShuffleChecked);
        }
        public void SetShuffledQuestionList(ObservableCollection<QuestionModel> shuffledQuestionList)
        {

            ShuffledQuestionList = shuffledQuestionList;
            foreach (var question in shuffledQuestionList)
            {
                if (!_loadedQuestionList.Any(q => q.Id == question.Id))
                {
                    _loadedQuestionList.Add(question);
                }
            }
        }
        public void SetIsShuffleChecked(bool isShuffleChecked)
        {
            IsShuffleChecked = isShuffleChecked;
        }

    }
}

