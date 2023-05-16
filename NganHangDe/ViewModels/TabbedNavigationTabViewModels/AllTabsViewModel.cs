using NganHangDe.Commands;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.ViewModels.TabbedNavigationTabViewModels
{
    public class AllTabsViewModel:ViewModelBase
    {
       


        private QuestionsTabViewModel _questionsTabViewModel;
        public QuestionsTabViewModel QuestionsTabViewModel
        {
            get
            {
                return _questionsTabViewModel;
            }
            set
            {
                _questionsTabViewModel = value;
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
        public AllTabsViewModel(NavigationStore ancestorNavigationStore)
        {
            QuestionsTabViewModel =   new QuestionsTabViewModel(ancestorNavigationStore);
            CategoriesTabViewModel = new CategoriesTabViewModel();
        }
    }
}
