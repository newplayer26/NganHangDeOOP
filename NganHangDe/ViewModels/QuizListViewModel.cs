using System.Collections.ObjectModel;
using System.Windows.Input;
using NganHangDe.Commands;
using NganHangDe.DataAccess;
using NganHangDe.Models;
using NganHangDe.Services;

namespace NganHangDe.ViewModels
{
    public class QuizListViewModel : ViewModelBase
    {
        private ObservableCollection<QuizViewModel> _quizzes;
        private readonly IQuizService _quizService;
        public ICommand QuizSelectedCommand { get; }
        private QuizViewModel _selectedQuiz;
        public QuizViewModel SelectedQuiz
        {
            get { return _selectedQuiz; }
            set
            {
                _selectedQuiz = value;
                OnPropertyChanged(nameof(SelectedQuiz));
            }
        }
        public ObservableCollection<QuizViewModel> Quizzes
        {
            get { return _quizzes; }
            set
            {
                _quizzes = value;
                OnPropertyChanged(nameof(Quizzes));
            }
        }

        public QuizListViewModel()
        {
            _quizService = new QuizService(new AppDbContext());
            LoadQuizzes();
            QuizSelectedCommand = new QuizSelectedCommand(QuizSelected);
        }

        private async void LoadQuizzes()
        {
            var quizList = await _quizService.GetAllQuizzesAsync();
            Quizzes = new ObservableCollection<QuizViewModel>(quizList);
        }
        
        private void QuizSelected()
        {
            //TODO: 
        }
    }
}
