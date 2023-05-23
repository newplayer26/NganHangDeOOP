using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.ViewModels.TabbedNavigationTabViewModels
{
    public class CategoriesTabViewModel:ViewModelBase
    {
        public ICommand SubmitCategoryCommand { get; set; }
        public ICommand LoadCategoriesCommand { get; }
        public CategoriesTabViewModel(AllTabsViewModel parentViewModel)
        {
            
        }
    }
}
