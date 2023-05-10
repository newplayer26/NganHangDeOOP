using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.ViewModels.Components
{
    public class CategoryListViewModel : ViewModelBase
    {
        private ObservableCollection<CategoryModel> _categories;
        private readonly ICategoryService _categoryService;
        public ICommand CategorySelectedCommand { get; }
        private CategoryModel _selectedCategory;
        public CategoryModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public ObservableCollection<CategoryModel> Categories
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
            Categories = new ObservableCollection<CategoryModel>(categories);
        }
        public event EventHandler<CategoryModel> CategorySelectedEvent;
        private void CategorySelected()
        {
            CategorySelectedEvent?.Invoke(this, SelectedCategory);
        }

    }

}
