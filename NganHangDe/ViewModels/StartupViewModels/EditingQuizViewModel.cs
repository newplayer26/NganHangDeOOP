using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using NganHangDe.Commands;
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

namespace NganHangDe.ViewModels.StartupViewModels
{

    public class EditingQuizViewModel : ViewModelBase
    {
        private readonly NavigationStore _ancestorNavigationStore;
        private int _id;
        private QuizModel _quiz;
        public RelayCommand ToAddFromQuestionBankViewCommand { get; private set; }
        public RelayCommand ToAddARandomQuestionViewComamnd { get; private set; }
        public string QuizName
        {
            get { return _quiz.Name; }
            set
            {
                _quiz.Name = value;
                OnPropertyChanged(nameof(QuizName));
            }
        }
        public EditingQuizViewModel(NavigationStore ancestorNavigationStore, int id)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            _id = id;
            _quiz = new QuizModel();
            LoadQuiz();
            //Console.WriteLine(_id.ToString());  
            ToAddFromQuestionBankViewCommand = new RelayCommand(ExecuteAddFromQuestionBankViewCommand);
            ToAddARandomQuestionViewComamnd = new RelayCommand(ExecuteAddARandomQuestionViewCommand);
        }
        private async void LoadQuiz()
        {
            QuizService quizService = new QuizService();
            Quiz quiz = await quizService.GetFullQuizById(_id);

            if (quiz != null)
            {
                _quiz = new QuizModel { Id = quiz.Id, Name = quiz.Name, Description = quiz.Description };
                QuizName = _quiz.Name;
                //Console.WriteLine(_quiz.Name);
            }
        }
        private void ExecuteAddFromQuestionBankViewCommand(object parameter)
        {
            AddFromQuestionBankViewModel addFromQuestionBankViewModel = new AddFromQuestionBankViewModel(_ancestorNavigationStore, _id);
            _ancestorNavigationStore.CurrentViewModel = addFromQuestionBankViewModel;
        }
        private void ExecuteAddARandomQuestionViewCommand(object parameter)
        {
            AddARandomQuestionViewModel addARandomQuestionViewModel = new AddARandomQuestionViewModel(_ancestorNavigationStore, _id);
            _ancestorNavigationStore.CurrentViewModel = addARandomQuestionViewModel;
        }
    }
}