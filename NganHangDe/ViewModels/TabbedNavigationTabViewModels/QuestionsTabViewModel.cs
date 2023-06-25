using NganHangDe.Commands;
using NganHangDe.Services;
using NganHangDe.Stores;
using NganHangDe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;


namespace NganHangDe.ViewModels.TabbedNavigationTabViewModels
{   
    public class QuestionsTabViewModel : ViewModelBase
    {
        private ObservableCollection<CategoryModel> _categoryList = new ObservableCollection<CategoryModel>();
        public IEnumerable<CategoryModel> CategoryList => _categoryList;
        public IEnumerable<QuestionModel> QuestionList => IsShowingDescendants? _descendantsCategoriesList:_singleCategoryList;
        public ICommand ToNewQuestionViewCommand { get; }
        public ICommand LoadCategoriesCommand { get; }
        public ICommand LoadQuestionsCommand { get; }
        public CategoryModel _selectedCategory;
        private bool _isShowingDescendants;
        public bool IsShowingDescendants
        {
            get
            {
                return _isShowingDescendants;
            }
            set
            {
                _isShowingDescendants = value;
                OnPropertyChanged(nameof(IsShowingDescendants));
                OnPropertyChanged(nameof(QuestionList));
            }
        }

        private ObservableCollection<QuestionModel> _singleCategoryList;

        public ObservableCollection<QuestionModel> SingleCategoryList
        {
            get { return _singleCategoryList; }
            set { _singleCategoryList = value; }
        }
        private ObservableCollection<QuestionModel> _descendantsCategoriesList;

        public ObservableCollection<QuestionModel> DescendantsCategoriesList
        {
            get { return _descendantsCategoriesList; }
            set { _descendantsCategoriesList = value; }
        }

        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }
            set {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    LoadQuestionsCommand.Execute(value.Id);
                }
            }
        }

        public QuestionsTabViewModel(AllTabsViewModel ancestorViewmodel)
        {
           
            ToNewQuestionViewCommand = new NavigateCommand<NewQuestionViewModel>(ancestorViewmodel.AncestorNavigationStore, typeof(NewQuestionViewModel));
            LoadCategoriesCommand = new GetCategoriesCommand(LoadCategories);
            LoadQuestionsCommand = new GetQuestionCommand(LoadQuestions);
            LoadCategoriesCommand.Execute(null);
        }
     
        public void LoadCategories(List<CategoryModel> list)
        {
            _categoryList.Clear();
            foreach (var category in list) {
                _categoryList.Add(category);
            }

        }

        public void LoadQuestions(List<QuestionModel> singleCategoryList, List<QuestionModel> descendantsCategoriesList)
        {
            SingleCategoryList = new ObservableCollection<QuestionModel>(singleCategoryList);
            DescendantsCategoriesList = new ObservableCollection<QuestionModel>(descendantsCategoriesList);
            OnPropertyChanged(nameof(QuestionList));
        }
     
    

    }  
}
