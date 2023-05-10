using NganHangDe.Commands;
using NganHangDe.DisplayModel;
using NganHangDe.Services;
using NganHangDe.ViewModels.StartUpViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Windows.Input;

namespace NganHangDe.ViewModels.Components
{
    public class CategoryListViewModel : ViewModelBase
    {
        private ObservableCollection<CategoryViewModel> _categories;
        private readonly ICategoryService _categoryService;
        public ICommand CategorySelectedCommand { get; }
        private CategoryViewModel _selectedCategory;
        public CategoryViewModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public ObservableCollection<CategoryViewModel> Categories
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
            Categories = new ObservableCollection<CategoryViewModel>(categories);
        }
        public event EventHandler<CategoryModel> CategorySelectedEvent;
        private void CategorySelected()
        {
            CategorySelectedEvent?.Invoke(this, SelectedCategory);
        }

    }

}
