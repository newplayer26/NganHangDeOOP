using NganHangDe.Commands;
using NganHangDe.Extensions;
using NganHangDe.Models;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace NganHangDe.ViewModels.TabbedNavigationTabViewModels
{
    public class NewQuestionViewModel:ViewModelBase
    {
        private ObservableCollection<ItemViewModel> _choices;

        public string Title => IsEditingQuestion? "Editing "+ " " + Question.Name: "Adding a Multiple choice question";
        public bool IsEditingQuestion;
        public ObservableCollection<ItemViewModel> Choices
        {
            get { return _choices; }
            set
            {
                _choices = value;
                OnPropertyChanged(nameof(Choices));
            }
        }

        public QuestionModel Question = new QuestionModel();

        public String QuestionName { get { return Question.Name; }
            set
            {
                Question.Name = value; 
                OnPropertyChanged(nameof(CanCreateQuestion));
            }
        }
        public String QuestionText
        {
            get { return Question.Text; }
            set
            {
                Question.Text = value;
                OnPropertyChanged(nameof(CanCreateQuestion));
            }
        }

        public bool CanCreateQuestion
        {
            get
            {
                double tmp = 0;
                foreach (var answerModel in ValidatedAnswers)
                {
                    if (answerModel.Grade > 0) tmp += answerModel.Grade;
                }
                return !StringExtensions.IsNullOrEmptyOrWhiteSpace(QuestionText) && !StringExtensions.IsNullOrEmptyOrWhiteSpace(QuestionName) && ValidatedAnswers.Count >= 2 && SelectedCategory != null &&  tmp == 1;
            }
        }

        private readonly NavigationStore _ancestorNavigationStore;

        private ObservableCollection<CategoryModel> _categoryList = new ObservableCollection<CategoryModel>();
        public IEnumerable<CategoryModel> CategoryList => _categoryList;
        public CategoryModel _selectedCategory;
        public ICommand ToAllTabsViewCommand { get; }
        public ICommand LoadCategoriesCommand { get; }
        public ICommand SubmitQuestionCommand { get; }
        public ICommand LoadSingleQuestionCommand { get; }
        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    OnPropertyChanged(nameof(CanCreateQuestion));
                }
            }
        }

        public NewQuestionViewModel(NavigationStore ancestorNavigationStore)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            LoadCategoriesCommand = new GetCategoriesCommand(LoadCategories);
            LoadSingleQuestionCommand = new LoadSingleQuestionCommand(LoadQuestion);
            _choices = new ObservableCollection<ItemViewModel>();
            AddChoicesCommand = new RelayCommand(Add3Choices);
            _choices = new ObservableCollection<ItemViewModel>();
            _choices.Add(new ItemViewModel { Number = "Choice 1", ParentViewModel = this });
            _choices.Add(new ItemViewModel { Number = "Choice 2" , ParentViewModel = this });
            SubmitQuestionCommand = new SubmitQuestionCommand(this);
            AfterCreateStays = _afterCreateStays;
            AfterCreateRedirects = _afterCreateRedirects;
            ToAllTabsViewCommand = new NavigateCommand<AllTabsViewModel>(_ancestorNavigationStore, typeof(AllTabsViewModel));
            IsEditingQuestion = false;
        }
        public void LoadQuestion (QuestionModel question, List<AnswerModel> answerList)
        {
            Question = question;
            _choices.Clear();
            _selectedCategory = new CategoryModel { Id = question.CategoryId };
            LoadCategoriesCommand.Execute(null);
            foreach (AnswerModel answer in answerList)
            {
                _choices.Add(new ItemViewModel { Number = $"Choice {answerList.IndexOf(answer) + 1}", ParentViewModel = this, Model = answer});
            }
            i = answerList.Count + 1;
            IsEditingQuestion = true;
            OnPropertyChanged(nameof(IsEditingQuestion));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Choices));
            OnPropertyChanged(nameof(QuestionName));
            OnPropertyChanged(nameof(QuestionText));
            OnPropertyChanged(nameof(ValidatedAnswers));
            OnPropertyChanged(nameof(CanCreateQuestion));
        }

        public List<AnswerModel> ValidatedAnswers =>    
          Choices
            .Select(c => c.Model)
            .Where(m => !StringExtensions.IsNullOrEmptyOrWhiteSpace(m.Text) && m.Text.Length > 0 && m.Grade != null)
            .ToList();
                
         

        public void LoadCategories(List<CategoryModel> list)
        {

            int Id =  _selectedCategory != null? _selectedCategory.Id: -1;
            _categoryList.Clear();
            foreach (var category in list)
            {
                _categoryList.Add(category);
                if (category.Id == Id)
                {
                    _selectedCategory = category;
                    OnPropertyChanged(nameof(SelectedCategory));
                }
            }
            OnPropertyChanged(nameof(_categoryList));
        }
        public static NewQuestionViewModel LoadViewModelWithCategory(NavigationStore ancestorNavigationStore, CategoryModel selectedCategory)
        {
            NewQuestionViewModel viewModel =  NewQuestionViewModel.LoadViewModel(ancestorNavigationStore);
            viewModel._selectedCategory = selectedCategory;
            return viewModel;
        }
        public static NewQuestionViewModel LoadViewModelWithQuestion(NavigationStore ancestorNavigationStore, QuestionModel questionModel)
        {
            NewQuestionViewModel viewModel = NewQuestionViewModel.LoadViewModel(ancestorNavigationStore);
            viewModel.LoadSingleQuestionCommand.Execute(questionModel.Id);
            return viewModel;
        }
        public static NewQuestionViewModel LoadViewModel(NavigationStore ancestorNavigationStore)
        {

            NewQuestionViewModel viewModel = new NewQuestionViewModel(ancestorNavigationStore);
            viewModel.LoadCategoriesCommand.Execute(null);
            return viewModel;
        }

        private int _counter = 3;
        
        public ICommand AddChoicesCommand { get; }
        int i = 3;
        private void Add3Choices(object parameter)
        {

            for (int i = 1; i <= 3; i++)
                _choices.Add(new ItemViewModel { Number = "Choice " + _counter++.ToString(), ParentViewModel = this });
        }
        private void _afterCreateStays(QuestionModel returnedQuestionModel, List<AnswerModel> returnedAnswerList)
        {
            IsEditingQuestion = true;
            Question = returnedQuestionModel;
            ObservableCollection<ItemViewModel> newItems = new ObservableCollection<ItemViewModel>();
            foreach(var answer in returnedAnswerList)
            {
                newItems.Add(new ItemViewModel { Number = $"Choice {returnedAnswerList.IndexOf(answer) + 1}", ParentViewModel = this, Model = answer });
            }
            _choices = newItems;
            LoadCategoriesCommand.Execute(null);
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(CanCreateQuestion));
        }

        public Action<QuestionModel, List<AnswerModel>> AfterCreateStays { get; set; } 
        private void _afterCreateRedirects()
        {
            ToAllTabsViewCommand.Execute(null); 
        }
        public Action AfterCreateRedirects { get; set;}
    }
    public class ItemViewModel:ViewModelBase
    {
        public string Number { get; set; }
        public AnswerModel Model = new AnswerModel();
        public NewQuestionViewModel ParentViewModel;
        public string Grade { get
            {
                if (Model.Grade == 0) return "None";
                else
                {
                    return $"{Model.Grade * 100}%";
                }
            }
            set
            {
                if(value == "None")
                {
                    Model.Grade = 0;
                }
                else
                {
                    string tmp = value.Substring(0, value.Length - 1);
                    Model.Grade = double.Parse(tmp) / 100;
                }
                ParentViewModel.QuestionName = ParentViewModel.QuestionName;
            }   
        }
        public string Text
        {
            get
            {
                return Model.Text;
                
            }
            set
            {
                Model.Text = value;
                ParentViewModel.QuestionName = ParentViewModel.QuestionName;
            }
        }
        private List<string> _gradeList = new List<string>();
        public ItemViewModel()
        {
            _gradeList = new List<string>(new string[] { "None", "100%", "90%", "83.33333%", "80%", "75%", "70%", "66.66667%", "60%", "50%", "40%", "33.33333%", "30%", "25%", "20%", "16.66667%", "14.28571%", "12.5%", "11.11111%", "10%", "5%", "-5%", "-10%", "-11.11111%", "-12.5%", "-14.28571%", "-16.66667%", "-20%", "-25%", "-30%", "-33.33333%", "-40%", "-50%", "-60%", "-66.66667%", "-70%", "-75%", "-80%", "-83.33333%" }); 
        }
        public IEnumerable<string> GradeList => _gradeList;
    }
}
