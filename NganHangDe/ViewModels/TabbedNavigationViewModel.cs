using NganHangDe.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NganHangDe.Stores;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;

namespace NganHangDe.ViewModels
{
    public class TabbedNavigationViewModel:ViewModelBase
    {
        private QuestionsTabViewModel questionsTabViewModel;
        public QuestionsTabViewModel QuestionsTabViewModel
        {
            get
            {
                return questionsTabViewModel;
            }
            set
            {
                questionsTabViewModel = value;
                OnPropertyChanged(nameof(QuestionsTabViewModel));
            }
        }
        private CategoriesTabViewModel _categoriesTabViewModel;
        public CategoriesTabViewModel CategoriesTabViewModel
        {
            get
            {
                return _categoriesTabViewModel;
            }
            set
            {
                _categoriesTabViewModel = value;
                OnPropertyChanged(nameof(CategoriesTabViewModel));
            }
        }

        private readonly NavigationStore _navigationStore;
        public ICommand ToStartupViewCommand { get; }
        public TabbedNavigationViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            QuestionsTabViewModel = new QuestionsTabViewModel();
            CategoriesTabViewModel = new CategoriesTabViewModel();
            ToStartupViewCommand = new NavigateCommand<StartupViewModel>(navigationStore, typeof(StartupViewModel));
        }
    }
}
