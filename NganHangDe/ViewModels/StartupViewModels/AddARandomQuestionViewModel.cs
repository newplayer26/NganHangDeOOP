using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.ViewModels.StartupViewModels
{
    class AddARandomQuestionViewModel : ViewModelBase
    {
        private readonly NavigationStore _ancestorNavigationStore;
        private int _quizId;
        private QuizService _quizService;
        public RelayCommand ToEditingQuizViewCommand { get; private set; }
        public RelayCommand SelectQuestionCommand { get; private set; }
        private ObservableCollection<CategoryModel> _categoryList = new ObservableCollection<CategoryModel>();
        public IEnumerable<CategoryModel> CategoryList => _categoryList;
        public IEnumerable<QuestionModel> QuestionList => IsShowingDescendants ? _descendantsCategoriesList : _singleCategoryList;
        private int _selectedNumber;
        public int SelectedNumber
        {
            get
            {
                return _selectedNumber;
            }
            set
            {
                _selectedNumber = value;
                OnPropertyChanged(nameof(SelectedNumber));
            }
        }
        public List<int> NumberOfQuestions
        {
            get
            {
                List<int> numberOfQuestions = new List<int>();
                if (QuestionList != null)
                {
                    for (int i = 0; i <= QuestionList.Count(); i++)
                    {
                        numberOfQuestions.Add(i);
                    }
                }
                return numberOfQuestions;
            }
        }    

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
                OnPropertyChanged(nameof(NumberOfQuestions));
                SelectedNumber = 0;
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

        public AddARandomQuestionViewModel(NavigationStore ancestorNavigationStore, int quizId)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            _quizId = quizId;
            _quizService = new QuizService();
            LoadCategoriesCommand = new GetCategoriesCommand(LoadCategories);
            LoadQuestionsCommand = new GetUnassignedQuestionsCommand(LoadQuestions, quizId);
            LoadCategoriesCommand.Execute(null);
            SelectQuestionCommand = new RelayCommand(ExecuteSelectQuestionCommand);
        }

        public void LoadCategories(List<CategoryModel> list)
        {
            foreach (var category in list)
            {
                _categoryList.Add(category);
            }
        }
        public void LoadQuestions(List<QuestionModel> singleCategoryList, List<QuestionModel> descendantsCategoriesList)
        {
            SingleCategoryList = new ObservableCollection<QuestionModel>(singleCategoryList);
            DescendantsCategoriesList = new ObservableCollection<QuestionModel>(descendantsCategoriesList);
            OnPropertyChanged(nameof(QuestionList));
            OnPropertyChanged(nameof(NumberOfQuestions));
        }
        private async void ExecuteSelectQuestionCommand(object parameter)
        {
            await _quizService.AddRandomQuestionsToQuizAsync(SelectedNumber, QuestionList.Select(q => q.Id).ToList(), _quizId);
            EditingQuizViewModel editingQuizViewModel = new EditingQuizViewModel(_ancestorNavigationStore, _quizId);
            _ancestorNavigationStore.CurrentViewModel = editingQuizViewModel;
        }
    }
}
