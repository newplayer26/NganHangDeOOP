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
using System.Windows.Input;

namespace NganHangDe.ViewModels.StartupViewModels
{

    public class AddFromQuestionBankViewModel : ViewModelBase
    {
        private readonly NavigationStore _ancestorNavigationStore;
        private int _id;
        public RelayCommand ToEditingQuizViewCommand { get; private set; }
        public RelayCommand SelectQuestionCommand { get; private set; }
        private ObservableCollection<CategoryModel> _categoryList = new ObservableCollection<CategoryModel>();
        private ObservableCollection<QuestionModel> _selectedQuestions = new ObservableCollection<QuestionModel>();
        public class SelectedQuestionsStore
        {
            public int QuizId { get; private set; }
            public static List<QuestionModel> SelectedQuestions { get; set; } = new List<QuestionModel>();
            public SelectedQuestionsStore(int quizId)
            {
                QuizId = quizId;
                SelectedQuestions = new List<QuestionModel>();
            }

            public void AddSelectedQuestion(QuestionModel question)
            {
                SelectedQuestions.Add(question);
            }
        }
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
        public ObservableCollection<QuestionModel> SelectedQuestions
        {
            get { return new ObservableCollection<QuestionModel>(SelectedQuestionsStore.SelectedQuestions); }
            set
            {
                SelectedQuestionsStore.SelectedQuestions = value.ToList();
                OnPropertyChanged(nameof(SelectedQuestions));
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
        public AddFromQuestionBankViewModel(NavigationStore ancestorNavigationStore, int id)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            _id = id;
            //ToEditingQuizViewCommand = new RelayCommand(ExecuteToEditingQuizViewCommand);
            LoadCategoriesCommand = new GetCategoriesCommand(LoadCategories);
            LoadQuestionsCommand = new GetQuestionCommand(LoadQuestions);
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
            SelectedQuestions.Clear();
            SingleCategoryList = new ObservableCollection<QuestionModel>(singleCategoryList);
            DescendantsCategoriesList = new ObservableCollection<QuestionModel>(descendantsCategoriesList);
            //Console.WriteLine("LoadQuestions");
            if (IsShowingDescendants)
            {
                foreach (var question in descendantsCategoriesList)
                {
                    SelectedQuestionsStore.SelectedQuestions.Add(question);
                }
            }
            else
            {
                foreach (var question in singleCategoryList)
                {
                    SelectedQuestionsStore.SelectedQuestions.Add(question);
                    //Console.WriteLine("add "+question.Text);
                }
            }

            OnPropertyChanged(nameof(QuestionList));
        }

        private void ExecuteSelectQuestionCommand(object parameter)
        {
            var question = parameter as QuestionModel;
            if (question != null)
            {
                if (question.IsSelected)
                {
                    SelectedQuestionsStore.SelectedQuestions.Add(question);
                    Console.WriteLine("add question");
                }
                else
                {
                    SelectedQuestionsStore.SelectedQuestions.Remove(question);
                    Console.WriteLine("remove question");
                }
            }

            int id = _id;
            Console.WriteLine($"{id} is selected");
            EditingQuizViewModel editingQuizViewModel = new EditingQuizViewModel(_ancestorNavigationStore, _id);
            editingQuizViewModel.LoadSelectedQuestions(SelectedQuestionsStore.SelectedQuestions.ToList(), id);
            _ancestorNavigationStore.CurrentViewModel = editingQuizViewModel;
        }
    }
}
