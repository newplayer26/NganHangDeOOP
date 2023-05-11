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
        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;
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
        public GUI12ViewModel(ICategoryService categoryService, IQuestionService questionService)
        {
            _categoryService = categoryService;
            _questionService = questionService;
            CategoryList = new CategoryListViewModel(_categoryService);
            CategoryList.CategorySelectedEvent += LoadCategoryQuestions;
            CategoryQuestions = new CategoryQuestionsViewModel(_questionService, -1);
            CategoryQuestions.QuestionSelectedEvent += OpenQuestionDetails;
        }
        private void LoadCategoryQuestions(object? sender, CategoryModel selectedCategory)
        {
            CategoryQuestions = new CategoryQuestionsViewModel(_questionService, selectedCategory.Id);
            CategoryQuestions.QuestionSelectedEvent += OpenQuestionDetails;
        }
        private void OpenQuestionDetails(object? sender, QuestionModel selectedQuestion)
        {
            // Execute the code to open a new window with the selected question as a parameter
        }
    }
}
