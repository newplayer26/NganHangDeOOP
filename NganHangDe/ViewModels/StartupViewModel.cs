
using System;
using System.Windows.Input;
using NganHangDe.Commands;
using NganHangDe.Stores;
using NganHangDe.DataAccess;
using NganHangDe.Services;
//using NganHangDe.ViewModels.Components;

namespace NganHangDe.ViewModels
{
    public class StartupViewModel : ViewModelBase
    {
        public ICommand ToTabbedViewCommand { get; }
        private readonly NavigationStore _navigationStore;

		public StartupViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            
            ToTabbedViewCommand = new NavigateCommand<TabbedNavigationViewModel>(navigationStore, typeof(TabbedNavigationViewModel));
            QuizService quizService = new QuizService();
            //QuizListViewModel quizListViewModel = new QuizListViewModel(quizService);
            
        }
        
    }
}
