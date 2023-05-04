
using System;
using System.Windows.Input;
using NganHangDe.Commands;
using NganHangDe.Stores;

namespace NganHangDe.ViewModels
{
    public class StartupViewModel:ViewModelBase
    {
        public ICommand ToTabbedViewCommand { get; }
        private readonly NavigationStore _navigationStore;
		public StartupViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            
            ToTabbedViewCommand = new NavigateCommand<TabbedNavigationViewModel>(navigationStore, typeof(TabbedNavigationViewModel));
        }

    }
}
