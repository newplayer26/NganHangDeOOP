using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.ViewModels.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.ViewModels
{
    public class GUI12ViewModel : ViewModelBase
    {
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly QuestionService _questionService = new QuestionService();
        private CategoryListViewModel _categoryList;
        public CategoryListViewModel CategoryList
        {
            get => _categoryList;
            set
            {
                _categoryList = value;
                OnPropertyChanged(nameof(CategoryList));
            }
        }
        private CategoryQuestionsViewModel _categoryQuestions;
        public CategoryQuestionsViewModel CategoryQuestions
        {
            get => _categoryQuestions;
            set
            {
                _categoryQuestions = value;
                OnPropertyChanged(nameof(CategoryQuestions));
            }
        }
        public GUI12ViewModel()
        {
            CategoryList = new CategoryListViewModel(_categoryService);
            CategoryList.CategorySelectedEvent += LoadCategoryQuestions;
            CategoryQuestions = new CategoryQuestionsViewModel(_questionService, -1);
        }
        private void LoadCategoryQuestions(object? sender, CategoryModel selectedCategory)
        {
            CategoryQuestions = new CategoryQuestionsViewModel(_questionService, selectedCategory.Id);
        }
    }
}
