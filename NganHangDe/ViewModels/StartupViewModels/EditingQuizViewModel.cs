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
        public EditingQuizViewModel(NavigationStore ancestorNavigationStore, int id)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            _id = id;
            LoadQuiz();
            Console.WriteLine(_id.ToString());
        }
        private async void LoadQuiz()
        {
            QuizService quizService = new QuizService();
            Quiz quiz = await quizService.GetFullQuizById(_id);

            if (quiz != null)
            {
                _quiz = new QuizModel
                {
                    Id = quiz.Id,
                    Name = quiz.Name,
                    Description = quiz.Description
                };

              
            }
        }
    }
}