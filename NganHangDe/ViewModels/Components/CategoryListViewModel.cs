using NganHangDe.Commands;
using NganHangDe.DisplayModel;
using NganHangDe.Services;

using System.Collections.ObjectModel;

using System.Windows.Input;

namespace NganHangDe.ViewModels.Components
{
    public class CategoryListViewModel : ViewModelBase
    {
        private ObservableCollection<CategoryDisplayModel> _categories;
        private readonly ICategoryService _categoryService;
        public ICommand CategorySelectedCommand { get; }
        private CategoryDisplayModel _selectedCategory;
        public CategoryDisplayModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public ObservableCollection<CategoryDisplayModel> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        public CategoryListViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            LoadCategories();
            CategorySelectedCommand = new CategorySelectedCommand(CategorySelected);
        }

        private async void LoadCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            Categories = new ObservableCollection<CategoryDisplayModel>(categories);
        }
        private void CategorySelected()
        {
            //TODO: 
        }
    }

}
