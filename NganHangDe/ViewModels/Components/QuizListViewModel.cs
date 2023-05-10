using System.Collections.ObjectModel;
using System.Windows.Input;
using NganHangDe.Commands;
using NganHangDe.DataAccess;
using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.ViewModels;

namespace NganHangDe.ViewModels.Components
{
    public class QuizListViewModel : ViewModelBase
    {
        private ObservableCollection<QuizModel> _quizzes;
        private readonly IQuizService _quizService;
        public ICommand QuizSelectedCommand { get; }
        private QuizModel _selectedQuiz;
        public QuizModel SelectedQuiz
        {
            get { return _selectedQuiz; }
            set
            {
                _selectedQuiz = value;
                OnPropertyChanged(nameof(SelectedQuiz));
            }
        }
        public ObservableCollection<QuizModel> Quizzes
        {
            get { return _quizzes; }
            set
            {
                _quizzes = value;
                OnPropertyChanged(nameof(Quizzes));
            }
        }

        public QuizListViewModel(IQuizService QuizService)
        {
            _quizService = QuizService;
            LoadQuizzes();
            QuizSelectedCommand = new QuizSelectedCommand(QuizSelected);
        }

        private async void LoadQuizzes()
        {
            var quizList = await _quizService.GetAllQuizzesAsync();
            Quizzes = new ObservableCollection<QuizModel>(quizList);
        }

        private void QuizSelected()
        {
            //TODO: 
        }
    }
}
