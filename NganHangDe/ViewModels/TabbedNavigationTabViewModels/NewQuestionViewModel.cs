using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.ViewModels.TabbedNavigationTabViewModels
{
    public class NewQuestionViewModel:ViewModelBase
    {
        private ObservableCollection<CategoryModel> _categoryList = new ObservableCollection<CategoryModel>();
        public IEnumerable<CategoryModel> CategoryList => _categoryList;
        public CategoryModel _selectedCategory;
        public ICommand LoadCategoriesCommand { get; }
        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                }
            }
        }

        public NewQuestionViewModel(NavigationStore ancestorNavigationStore, CategoryModel selectedCategory)
        {
            _selectedCategory = selectedCategory;
            LoadCategoriesCommand = new GetCategoriesCommand(LoadCategories);
           
        }
        public void LoadCategories(List<CategoryModel> list)
        {

            foreach (var category in list)
            {
                _categoryList.Add(category);
                if(category.Id == _selectedCategory.Id)
                {
                    SelectedCategory = category;
                    OnPropertyChanged(nameof(SelectedCategory));
                }
            }


        }
        public static NewQuestionViewModel LoadViewModel(NavigationStore ancestorNavigationStore, CategoryModel selectedCategory)
        {

            NewQuestionViewModel viewModel = new NewQuestionViewModel(ancestorNavigationStore, selectedCategory);
            viewModel.LoadCategoriesCommand.Execute(null);
            return viewModel;
        }
    }
}
