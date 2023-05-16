
using System;
using System.Windows.Input;
using NganHangDe.Commands;
using NganHangDe.Stores;
using NganHangDe.DataAccess;
using NganHangDe.Services;
using NganHangDe.ViewModels.StartupViewModels;

namespace NganHangDe.ViewModels
{
    public class StartupViewModel : ViewModelBase
    {
        public ICommand ToTabbedViewCommand { get; }
        private readonly NavigationStore _navigationStore;
        private readonly NavigationStore _ownNavigationStore;
        public ViewModelBase CurrentViewModel => _ownNavigationStore.CurrentViewModel;
        public StartupViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            
            ToTabbedViewCommand = new NavigateCommand<TabbedNavigationViewModel>(navigationStore, typeof(TabbedNavigationViewModel));
            QuizService quizService = new QuizService();
            _ownNavigationStore = new NavigationStore();
            _ownNavigationStore.CurrentViewModel = new AllQuizzesViewModel(_ownNavigationStore);

            //QuizListViewModel quizListViewModel = new QuizListViewModel(quizService);
            
        }
        
    }
}
