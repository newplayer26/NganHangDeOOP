using Microsoft.Identity.Client;
using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.ModelsDb;
using NganHangDe.Services;
using NganHangDe.Stores;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
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
        //private ObservableCollection<AnswerModel> _selectedCorrectAnswers;
        //public ObservableCollection<AnswerModel> SelectedCorrectAnswers
        //{
        //    get { return _selectedCorrectAnswers; }
        //    set
        //    {
        //        _selectedCorrectAnswers = value;
        //        OnPropertyChanged(nameof(SelectedCorrectAnswers));
        //    }
        //}
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
        private int _selectedAnswerScore;
        public int SelectedAnswerScore
        {
            get { return _selectedAnswerScore; }
            set
            {
                _selectedAnswerScore = value;
                OnPropertyChanged(nameof(SelectedAnswerScore));
            }
        }
        private double _totalGrade;
        public double TotalGrade
        {
            get { return _totalGrade; }
            set
            {
                _totalGrade = value;
                OnPropertyChanged(nameof(TotalGrade));
            }
        }
        private bool _isFinishAttemptClicked;
        public bool IsFinishAttemptClicked
        {
            get { return _isFinishAttemptClicked; }
            set
            {
                _isFinishAttemptClicked = value;
                OnPropertyChanged(nameof(IsFinishAttemptClicked));
            }
        }
        private DateTime startTime;
        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        private DateTime finishTime;
        public DateTime FinishTime
        {
            get { return finishTime; }
            set
            {
                finishTime = value;
                OnPropertyChanged(nameof(FinishTime));
            }
        }
        public TimeSpan TimeTaken => FinishTime - StartTime;
        private double _totalAnswerGrade;
        public double TotalAnswerGrade
        {
            get { return _totalAnswerGrade; }
            set
            {
                _totalAnswerGrade = value;
                OnPropertyChanged(nameof(TotalAnswerGrade));
            }
        }
        private double _scoreOutOfTen;
        public double ScoreOutOfTen
        {
            get { return _scoreOutOfTen; }
            set
            {
                _scoreOutOfTen = value;
                OnPropertyChanged(nameof(ScoreOutOfTen));
            }
        }
        private double _percentage;
        public double Percentage
        {
            get { return _percentage; }
            set
            {
                _percentage = value;
                OnPropertyChanged(nameof(Percentage));
            }
        }
        private ObservableCollection<AnswerModel> _correctAnswers;

        public ObservableCollection<AnswerModel> CorrectAnswers
        {
            get { return _correctAnswers; }
            set
            {
                _correctAnswers = value;
                OnPropertyChanged(nameof(CorrectAnswers));
            }
        }
        public RelayCommand FinishAttemptCommand { get; set; }
        public PreviewQuizViewModel(NavigationStore ancestorNavigationStore, int quizId, bool isShuffleChecked, ObservableCollection<QuestionModel> shuffledQuestionList)
        {
            
            _isShuffleChecked = isShuffleChecked;
            _shuffledQuestionList = shuffledQuestionList;
            _ancestorNavigationStore = ancestorNavigationStore;
            _quizId = quizId;
            FinishAttemptCommand = new RelayCommand(FinishAttempt);
            _quizService = new QuizService();
            _ = LoadQuestionsAsync();
            
        }
        private async Task LoadQuestionsAsync()
        {
            StartTime = DateTime.Now;
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
                            Answers = question.Answers,
                            
                        };
                        var loadQuestionCommand = new LoadSingleQuestionCommand(LoadQuestionCallback);
                        
                        await loadQuestionCommand.ExecuteAsync(questionModel.Id);
                        
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
            //foreach (var answer in answers)
            //{
            //    var answerModel = new AnswerModel
            //    {
            //        Id = answer.Id,
            //        Grade = answer.Grade,
            //        AnswerGroup = question.Id
            //    };
            //}
            //Console.WriteLine(question.IsMultipleAnswers);
            _loadedQuestionList.Add(question);
            question.QuestionNumber = _questionNumber++;
            var correctAnswers = new ObservableCollection<AnswerModel>(answers.Where(answer => answer.Grade > 0));
            question.CorrectAnswers = correctAnswers;
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
        public void FinishAttempt(object parameter)
        {
            FinishTime = DateTime.Now;
            // Tính toán các câu trả lời đúng
            double totalGrade = 0;
            double totalAnswerGrade = 0; //tổng điểm của quiz 
            foreach (var question in LoadedQuestionList)
            {
                var selectedCorrectAnswers = question.Answers.Where(answer => answer.Grade > 0 && answer.IsSelected);
                question.SelectedCorrectAnswers = new ObservableCollection<AnswerModel>(selectedCorrectAnswers);
                double questionSelectedGrade = CalculateQuestionGrade(question);
                double questionGrade = CalculateAnswerGrade(question);
                totalGrade += questionSelectedGrade;
                totalAnswerGrade += questionGrade;
            }
            Console.WriteLine(totalGrade);
            TotalGrade = totalGrade;
            TotalAnswerGrade = totalAnswerGrade;
            ScoreOutOfTen = Math.Round((totalGrade / totalAnswerGrade)*10,2);
            Percentage = Math.Round((totalGrade/totalAnswerGrade)*100,2);
            IsFinishAttemptClicked = true;
        }
        private double CalculateQuestionGrade(QuestionModel question)
        {
            double totalGrade = 0;
            
            foreach (var answer in question.Answers)
            {
                if (answer.IsSelected)
                {
                    totalGrade += answer.Grade;
                }
            }
            return totalGrade;
        }
        private double CalculateAnswerGrade(QuestionModel question)
        {
            double totalAnswersGrade = 0;
            foreach(var answer in question.Answers)
            {
                totalAnswersGrade += answer.Grade;
            }
            return totalAnswersGrade;
        }
    }
}

