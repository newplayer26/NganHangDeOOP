using DocumentFormat.OpenXml.Office2010.Excel;
using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.ModelsDb;
using NganHangDe.Services;
using NganHangDe.Stores;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NganHangDe.ViewModels.StartupViewModels
{

    public class AddFromQuestionBankViewModel : ViewModelBase
    {
        private readonly NavigationStore _ancestorNavigationStore;
        private int _quizId;
        private QuizService _quizService;
        public RelayCommand ToEditingQuizViewCommand { get; private set; }
        public RelayCommand SelectQuestionCommand { get; private set; }
        private ObservableCollection<CategoryModel> _categoryList = new ObservableCollection<CategoryModel>();
        private ObservableCollection<QuestionModel> _selectedQuestions = new ObservableCollection<QuestionModel>();
        public ObservableCollection<QuestionModel> SelectedQuestions => _selectedQuestions;
        public IEnumerable<CategoryModel> CategoryList => _categoryList;
        public IEnumerable<QuestionModel> QuestionList => IsShowingDescendants ? _descendantsCategoriesList : _singleCategoryList;

        public CategoryModel _selectedCategory;
        public ICommand LoadCategoriesCommand { get; }
        public ICommand LoadQuestionsCommand { get; }
        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    LoadQuestionsCommand.Execute(value.Id);
                }
            }
        }
      
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
        private ObservableCollection<QuestionModel> _descendantsCategoriesList;

        public ObservableCollection<QuestionModel> DescendantsCategoriesList
        {
            get { return _descendantsCategoriesList; }
            set { _descendantsCategoriesList = value; }
        }

        public ObservableCollection<QuestionModel> SingleCategoryList
        {
            get { return _singleCategoryList; }
            set { _singleCategoryList = value; }
        }
        public AddFromQuestionBankViewModel(NavigationStore ancestorNavigationStore, int quizId)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            _quizId = quizId;
            _quizService = new QuizService();
            LoadCategoriesCommand = new GetCategoriesCommand(LoadCategories);
            LoadQuestionsCommand = new GetUnassignedQuestionsCommand(LoadQuestions, quizId);
            LoadCategoriesCommand.Execute(null);
            SelectQuestionCommand = new RelayCommand(ExecuteSelectQuestionCommand);
            ToEditingQuizViewCommand = new RelayCommand(ExecuteToEditingQuizViewCommand);
        }
        public void LoadCategories(List<CategoryModel> list)
        {
            _categoryList.Clear();
            foreach (var category in list)
            {
                _categoryList.Add(category);
            }
            OnPropertyChanged(nameof(CategoryList));
        }
        public void LoadQuestions(List<QuestionModel> singleCategoryList, List<QuestionModel> descendantsCategoriesList)
        {
            SingleCategoryList = new ObservableCollection<QuestionModel>(singleCategoryList);
            DescendantsCategoriesList = new ObservableCollection<QuestionModel>(descendantsCategoriesList);
            _selectedQuestions = new ObservableCollection<QuestionModel>();
            OnPropertyChanged(nameof(QuestionList));
        }
        private async void ExecuteSelectQuestionCommand(object parameter)
        {
            try 
            {
                List<QuestionModel> selectedQuestions = QuestionList.Where(q => q.IsSelected).ToList();
                _selectedQuestions.Clear();
                foreach (var question in selectedQuestions)
                {
                    _selectedQuestions.Add(question);
                    Console.WriteLine(question.Id);
                }
                foreach (var question in selectedQuestions)
                {
                    await _quizService.AddSingleQuestionToQuizAsync(question.Id, _quizId);
                }
                EditingQuizViewModel editingQuizViewModel = new EditingQuizViewModel(_ancestorNavigationStore, _quizId);
                _ancestorNavigationStore.CurrentViewModel = editingQuizViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select a category.");
            }          
        }
        private void ExecuteToEditingQuizViewCommand(object parameter)
        {
            int quizId = _quizId;
            Console.WriteLine(quizId);
            EditingQuizViewModel editingQuizViewModel = new EditingQuizViewModel(_ancestorNavigationStore, quizId);
            _ancestorNavigationStore.CurrentViewModel = editingQuizViewModel;
        }
    }
}
