using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Stores;
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
      


        private readonly NavigationStore _ancestorNavigationStore;
        public CategoriesTabViewModel(AllTabsViewModel parentViewModel)
        {
            
            _ancestorNavigationStore = parentViewModel.AncestorNavigationStore;
            LoadCategoriesCommand = new GetCategoriesCommand(LoadCategories);
            LoadCategoriesCommand.Execute(null);
        }
        public void LoadCategories (List<CategoryModel> list)
        {

        }
    }
}
