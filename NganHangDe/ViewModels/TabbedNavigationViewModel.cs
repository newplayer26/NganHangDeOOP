using NganHangDe.Commands;
using System.Windows.Input;
using NganHangDe.Stores;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;

namespace NganHangDe.ViewModels
{
    public class TabbedNavigationViewModel:ViewModelBase
    {
      

        private readonly NavigationStore _navigationStore;
        private readonly NavigationStore _ownNavigationStore;
        public ViewModelBase CurrentViewModel => _ownNavigationStore.CurrentViewModel;
        public ICommand ToStartupViewCommand { get; }
        public ICommand ToNewQuestionViewCommand { get; }

        public TabbedNavigationViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _ownNavigationStore = new NavigationStore();
            //ToNewQuestionViewCommand = new NavigateCommand<NewQuestionViewModel>(_ownNavigationStore, typeof(NewQuestionViewModel));
            ToStartupViewCommand = new NavigateCommand<StartupViewModel>(navigationStore, typeof(StartupViewModel));
            _ownNavigationStore.CurrentViewModel = new AllTabsViewModel(_ownNavigationStore);
            _ownNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            
            
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
