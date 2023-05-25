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
        public ICommand LoadCategoriesCommand { get; set; }
        private readonly NavigationStore _ancestorNavigationStore;
        public CategoriesTabViewModel(NavigationStore ancestorNavigationStore)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            LoadCategoriesCommand = new GetCategoriesCommand(LoadCategories);
            LoadCategoriesCommand.Execute(null);
        }
        public void LoadCategories (List<CategoryModel> list)
        {

        }
    }
}
