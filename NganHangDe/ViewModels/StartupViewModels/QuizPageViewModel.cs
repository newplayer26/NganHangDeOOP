using Microsoft.Identity.Client;
using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.ModelsDb;
using NganHangDe.Services;
using NganHangDe.Stores;
using NganHangDe.ViewModels.QuizUIViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private QuizModel _quiz;
        public QuizModel Quiz => _quiz;
        private IFileService _fileService = new FileService();
        private IQuizService _quizService = new QuizService();
        private string _formattedTimeLimit;
        public string FormattedTimeLimit
        {
            get { return _formattedTimeLimit; }
            set
            {
                _formattedTimeLimit = value;
                OnPropertyChanged(nameof(FormattedTimeLimit));
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));  
            }
        }
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

        private bool _isShuffleChecked = false;

        public bool IsShuffleChecked
        {
            get { return _isShuffleChecked; }
            set
            {
                _isShuffleChecked = value;
                OnPropertyChanged(nameof(IsShuffleChecked));
                // Cập nhật logic tương ứng khi giá trị IsShuffleChecked thay đổi

            }
        }

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
        public RelayCommand ShowPopupCommand { get; private set; }
        public RelayCommand HidePopupCommand { get; private set; }  
        public RelayCommand ToEditingQuizViewCommand { get; private set; }
        public RelayCommand ToPreviewQuizViewCommand { get; private set; }
        public ICommand ExportToPDFCommand { get; private set; }
        public ICommand ExportToPDFWithPasswordCommand { get; private set; }
        public QuizPageViewModel(NavigationStore ancestorNavigationStore, QuizModel model)
        {
            _model = model;
            _ancestorNavigationStore = ancestorNavigationStore;
            //ToEditingQuizViewCommand = new NavigateCommand<EditingQuizViewModel>(_ancestorNavigationStore, typeof(EditingQuizViewModel));
            ToEditingQuizViewCommand = new RelayCommand(ExecuteToEditingQuizViewCommand);
            ShowPopupCommand = new RelayCommand(ExecuteShowPopupCommand);
            HidePopupCommand = new RelayCommand(ExecuteHidePopupCommand);            
            ToPreviewQuizViewCommand = new RelayCommand(ExecuteToPreviewQuizViewCommand);
            ExportToPDFCommand = new ExportCommand(this);
            ExportToPDFWithPasswordCommand = new ExportWithPasswordCommand(this);
            LoadQuiz();
        }
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
        private void ExecuteHidePopupCommand(object parameter)
        {
            IsPopupVisible = false;
        }
        private void ExecuteToPreviewQuizViewCommand(object parameter)
        {
            int quizid = _model.Id;
            bool isShuffleChecked = _isShuffleChecked;
            ObservableCollection<QuestionModel> shuffledQuestionList = _shuffledQuestionList;
            Console.WriteLine("ShuffleQuizPage = " + IsShuffleChecked);
            PreviewQuizViewModel previewQuizViewModel = new PreviewQuizViewModel(_ancestorNavigationStore, quizid, isShuffleChecked, shuffledQuestionList);
            _ancestorNavigationStore.CurrentViewModel = previewQuizViewModel;
            
            Console.WriteLine(_isShuffleChecked);
        }
        public void SetShuffledQuestionList(ObservableCollection<QuestionModel> shuffledQuestionList)
        {
            ShuffledQuestionList = shuffledQuestionList;
            foreach (QuestionModel question in ShuffledQuestionList)
            {
                Console.WriteLine(question.Id);
            }
        }
        public void SetIsShuffledChecked(bool isShuffledChecked)
        {
            IsShuffleChecked = isShuffledChecked;
        }
        private async void LoadQuiz()
        {
            QuizService quizService = new QuizService();
            Quiz quiz = await quizService.GetFullQuizById(_model.Id);
            if (quiz != null)
            {
                _quiz = new QuizModel { Id = quiz.Id, Name = quiz.Name, Description = quiz.Description, TimeLimit=quiz.TimeLimit };
                FormattedTimeLimit = _quiz.TimeLimit.ToString("hh\\:mm\\:ss");
            }
            OnPropertyChanged(nameof(FormattedTimeLimit));
        }
    }
}