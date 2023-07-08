using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using NganHangDe.Commands;
using NganHangDe.Extensions;
using NganHangDe.Models;
using NganHangDe.ModelsDb;
using NganHangDe.Services;
using NganHangDe.Stores;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
using NganHangDe.Views.StartupViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static NganHangDe.ViewModels.StartupViewModels.AddFromQuestionBankViewModel;

namespace NganHangDe.ViewModels.StartupViewModels
{

    public class EditingQuizViewModel : ViewModelBase
    {
        private readonly NavigationStore _ancestorNavigationStore;
        private int _quizId;
        public int QuizId => _quizId;
        private int _questionNumber;
        private QuizModel _quiz;
        public event Action<ObservableCollection<QuestionModel>> QuestionListShuffled;
        public RelayCommand ToAddFromQuestionBankViewCommand { get; private set; }
        public RelayCommand ToAddARandomQuestionViewComamnd { get; private set; }
        public RelayCommand ToggleShuffleCommand { get; private set; }
        public RelayCommand DeleteQuestionCommand { get; private set; }
        public RelayCommand ToQuizPageViewCommand { get; private set; }
        public RelayCommand SelectMultipleItemsCommand { get; private set; }
        public ICommand DeleteSelectQuestionsCommand { get; set;  }
        private ObservableCollection<QuestionModel> _questionList = new ObservableCollection<QuestionModel>();
        public ObservableCollection<QuestionModel> QuestionList => _questionList;
        private ObservableCollection<QuestionModel> _selectedQuestions;
        public int QuestionCount => _questionList.Count;
        private bool _isSelectMultipleItemsClicked;
        public bool IsSelectMultipleItemsClicked
        {
            get { return _isSelectMultipleItemsClicked; }
            set
            {
                _isSelectMultipleItemsClicked = value;
                OnPropertyChanged(nameof(IsSelectMultipleItemsClicked));
            }
        }
        public ObservableCollection<QuestionModel> SelectedQuestions

        {
            get { return _selectedQuestions; }
            set
            {
                _selectedQuestions = value;
                OnPropertyChanged(nameof(SelectedQuestions));
            }
        }
        public string QuizName
        {
            get { return _quiz.Name; }
            set
            {
                _quiz.Name = value;
                OnPropertyChanged(nameof(QuizName));
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
                if (_isShuffleChecked)
                {
                    ShuffleAnswers();
                }                
            }
        }
        public EditingQuizViewModel(NavigationStore ancestorNavigationStore, int quizId)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            _quizId = quizId;
            _quiz = new QuizModel();
            ToAddFromQuestionBankViewCommand = new RelayCommand(ExecuteAddFromQuestionBankViewCommand);
            ToAddARandomQuestionViewComamnd = new RelayCommand(ExecuteAddARandomQuestionViewCommand);
            ToQuizPageViewCommand = new RelayCommand(ExecuteToQuizPageViewCommand);
            ToggleShuffleCommand = new RelayCommand(ExecuteToggleShuffleCommand);
            SelectMultipleItemsCommand = new RelayCommand(ExecuteSelectMultipleItemsCommand);
            DeleteSelectQuestionsCommand = new RemoveQuestionsFromQuizCommand(this);
            _ = LoadQuiz();
            
        }
        public async Task LoadQuiz()
        {
            QuizService quizService = new QuizService();
            Quiz quiz = await quizService.GetFullQuizById(_quizId);
            QuestionList.Clear();
            if (quiz != null)
            {
                _quiz = new QuizModel { Id = quiz.Id, Name = quiz.Name, Description = quiz.Description, };
                QuizName = _quiz.Name;
                
                List<QuestionModel> selectedQuestions = quiz.QuizQuestions
                    .Select(qq => new QuestionModel { Id = qq.Question.Id, Text = qq.Question.Text })
                    .ToList();
                foreach(QuestionModel question in selectedQuestions)
                {
                    var loadQuestionsCommand = new LoadSingleQuestionCommand(LoadQuestionCallback);
                    await loadQuestionsCommand.ExecuteAsync(question.Id);
                }
                SelectedQuestions = new ObservableCollection<QuestionModel>(selectedQuestions);
            }
            //Console.WriteLine(_questionList.Count);
            OnPropertyChanged(nameof(QuestionCount));
        }
        private void ExecuteAddFromQuestionBankViewCommand(object parameter)
        {
            AddFromQuestionBankViewModel addFromQuestionBankViewModel = new AddFromQuestionBankViewModel(_ancestorNavigationStore, _quizId);
            _ancestorNavigationStore.CurrentViewModel = addFromQuestionBankViewModel;
            
        }
        private void ExecuteAddARandomQuestionViewCommand(object parameter)
        {
            AddARandomQuestionViewModel addARandomQuestionViewModel = new AddARandomQuestionViewModel(_ancestorNavigationStore, _quizId);
            _ancestorNavigationStore.CurrentViewModel = addARandomQuestionViewModel;
        }
        private void LoadQuestionCallback(QuestionModel question, List<AnswerModel> answers)
        {
            question.Answers = answers;
            //Console.WriteLine(question.IsMultipleAnswers);
            _questionList.Add(question);
            var correctAnswers = new ObservableCollection<AnswerModel>(answers.Where(answer => answer.Grade > 0));
            question.CorrectAnswers = correctAnswers;
            //Console.WriteLine(question.Text);
        }
        private void ShuffleAnswers()
        {
            _questionNumber = 1;
            foreach (var question in _questionList)
            {
                if (question.Answers != null)
                {
                    question.QuestionNumber =_questionNumber;
                    _questionNumber++;
                    List<AnswerModel> shuffledAnswers = question.Answers.ToList();
                    shuffledAnswers.Shuffle();
                    question.Answers = new List<AnswerModel>(shuffledAnswers);
                    
                    //foreach(var answer in shuffledAnswers)
                    //{
                    //    Console.WriteLine(answer.Text);
                    //}
                }
                else Console.WriteLine("Nothing to Shuffle");
            }
            QuestionListShuffled?.Invoke(_questionList);
        }
        private void ExecuteToggleShuffleCommand(object parameter)
        {
            ShuffleAnswers();
        }
        private void ExecuteToQuizPageViewCommand(object parameter)
        {
            QuizPageViewModel quizPageViewModel = new QuizPageViewModel(_ancestorNavigationStore, _quiz);
            _ancestorNavigationStore.CurrentViewModel = quizPageViewModel;
            quizPageViewModel.SetShuffledQuestionList(_questionList);
            quizPageViewModel.SetIsShuffledChecked(_isShuffleChecked);           
        }
        private void ExecuteSelectMultipleItemsCommand(object parameter)
        {
            if (IsSelectMultipleItemsClicked)
            {
                _isSelectMultipleItemsClicked = false;
                OnPropertyChanged(nameof(IsSelectMultipleItemsClicked));
            }
            else
            {
                _isSelectMultipleItemsClicked = true;
                OnPropertyChanged(nameof(IsSelectMultipleItemsClicked));
            }
               
        }
    }
}