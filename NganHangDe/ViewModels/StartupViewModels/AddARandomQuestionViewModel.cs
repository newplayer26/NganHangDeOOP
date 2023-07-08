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
using System.Windows;
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
        public RelayCommand PreviousPageCommand { get; private set; }
        public RelayCommand NextPageCommand { get; private set; }
        public RelayCommand ChangePageCommand { get; private set; }
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
                    OnPropertyChanged(nameof(CanCreate));
                    LoadQuestionsCommand.Execute(value.Id);
                }
            }
        }
        public bool CanCreate => SelectedCategory != null; 
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
        private ObservableCollection<QuestionModel> _pagedQuestionList;
        public ObservableCollection<QuestionModel> PagedQuestionList
        {
            get { return _pagedQuestionList; }
            set
            {
                _pagedQuestionList = value;
                OnPropertyChanged(nameof(PagedQuestionList));
            }
        }

        private List<int> _pageNumbers;
        public List<int> PageNumbers
        {
            get { return _pageNumbers; }
            set
            {
                _pageNumbers = value;
                OnPropertyChanged(nameof(PageNumbers));
            }
        }

        private int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                UpdatePagedQuestionList();
            }
        }

        public AddARandomQuestionViewModel(NavigationStore ancestorNavigationStore, int quizId)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            _quizId = quizId;
            _quizService = new QuizService();
            LoadCategoriesCommand = new GetCategoriesWithUnassignedQuestionsCommand(LoadCategories, quizId);
            LoadQuestionsCommand = new GetUnassignedQuestionsCommand(LoadQuestions, quizId);
            LoadCategoriesCommand.Execute(null);
            SelectQuestionCommand = new RelayCommand(ExecuteSelectQuestionCommand);
            ToEditingQuizViewCommand = new RelayCommand(ExecuteToEditingQuizViewCommand);
            PreviousPageCommand = new RelayCommand(ExecutePreviousPageCommand);
            NextPageCommand = new RelayCommand(ExecuteNextPageCommand);
            ChangePageCommand = new RelayCommand(ExecuteChangePageCommand);
            UpdatePageNumbers();
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
            CurrentPage = 1;
            UpdatePagedQuestionList();
            UpdatePageNumbers();
            OnPropertyChanged(nameof(QuestionList));
            OnPropertyChanged(nameof(NumberOfQuestions));
            OnPropertyChanged(nameof(CurrentPage));
        }
        private async void ExecuteSelectQuestionCommand(object parameter)
        {
            try
            {
                await _quizService.AddRandomQuestionsToQuizAsync(SelectedNumber, QuestionList.Select(q => q.Id).ToList(), _quizId);
                EditingQuizViewModel editingQuizViewModel = new EditingQuizViewModel(_ancestorNavigationStore, _quizId);
                _ancestorNavigationStore.CurrentViewModel = editingQuizViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please try again.");
            }
           
        }
        private void ExecuteToEditingQuizViewCommand(object parameter)
        {
            int quizId = _quizId;
           // Console.WriteLine(quizId);
            EditingQuizViewModel editingQuizViewModel = new EditingQuizViewModel(_ancestorNavigationStore, quizId);
            _ancestorNavigationStore.CurrentViewModel = editingQuizViewModel;
        }
        private void UpdatePagedQuestionList()
        {
            int startIndex = (CurrentPage - 1) * 10;
            PagedQuestionList = new ObservableCollection<QuestionModel>(QuestionList.Skip(startIndex).Take(10));
        }

        private void UpdatePageNumbers()
        {
            if (QuestionList!=null)
            {
                int totalPages = (int)Math.Ceiling((double)QuestionList.Count() / 10);
                PageNumbers = Enumerable.Range(1, totalPages).ToList();
            }
            else
            {
                PageNumbers = new List<int>(); // Khởi tạo PageNumbers thành một danh sách rỗng
            }
        }
        private void ExecuteChangePageCommand(object parameter)
        {
            if (parameter is int page)
            {
                CurrentPage = page;
                UpdatePagedQuestionList();
            }
        }
        private void ExecutePreviousPageCommand(object parameter)
        {            
            if(CanExecutePreviousPageCommand(parameter))
            {
                CurrentPage--;
                Console.WriteLine(CurrentPage);
                UpdatePagedQuestionList();
            }                        
        }
        private void ExecuteNextPageCommand(object parameter)
        {
            if (CanExecuteNextPageCommand(parameter))
            {
                CurrentPage++;
                Console.WriteLine(CurrentPage);
                UpdatePagedQuestionList();
            }                        
        }
        private bool CanExecutePreviousPageCommand(object parameter)
        {
            return CurrentPage > 1;          
        }

        private bool CanExecuteNextPageCommand(object parameter)
        {
            return CurrentPage < PageNumbers.Count;
        }
    }
}
