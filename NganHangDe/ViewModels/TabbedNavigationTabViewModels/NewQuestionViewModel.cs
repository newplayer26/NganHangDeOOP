using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NganHangDe.ViewModels.TabbedNavigationTabViewModels
{
    public class NewQuestionViewModel:ViewModelBase
    {
        private ObservableCollection<ItemViewModel> _choices;

        public string Title => IsEditingQuestion? Question.Name: "Adding a Multiple choice question";
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

        public bool CanCreateQuestion => !string.IsNullOrEmpty(QuestionText) && !string.IsNullOrEmpty(QuestionName) && ValidatedAnswers.Count >= 2 && SelectedCategory != null && SelectedCategory.Id != null;

        private readonly NavigationStore _ancestorNavigationStore;

        private ObservableCollection<CategoryModel> _categoryList = new ObservableCollection<CategoryModel>();
        public IEnumerable<CategoryModel> CategoryList => _categoryList;
        public CategoryModel _selectedCategory;
        public ICommand ToAllTabsViewCommand { get; }
        public ICommand LoadCategoriesCommand { get; }
        public ICommand SubmitQuestionCommand { get; }
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

        public NewQuestionViewModel(NavigationStore ancestorNavigationStore, CategoryModel selectedCategory)
        {
            _selectedCategory = selectedCategory;
            _ancestorNavigationStore = ancestorNavigationStore;
            LoadCategoriesCommand = new GetCategoriesCommand(LoadCategories);
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

        public List<AnswerModel> ValidatedAnswers =>    
          Choices
            .Select(c => c.Model)
            .Where(m => !string.IsNullOrEmpty(m.Text) && m.Text.Length > 0 && m.Grade != null)
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
        public static NewQuestionViewModel LoadViewModel(NavigationStore ancestorNavigationStore, CategoryModel selectedCategory)
        {

            NewQuestionViewModel viewModel = new NewQuestionViewModel(ancestorNavigationStore, selectedCategory);
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
        private void _afterCreateStays()
        {
            IsEditingQuestion = true;

            LoadCategoriesCommand.Execute(null);
            OnPropertyChanged(nameof(Title));
        }

        public Action AfterCreateStays { get; set; } 
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
        public Double Grade { get
            {
                return Model.Grade;
            }
            set
            {
                Model.Grade = value;
                ParentViewModel.QuestionText = ParentViewModel.QuestionText;
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
                ParentViewModel.QuestionText = ParentViewModel.QuestionText;
            }
        }
        private List<Double> _gradeList = new List<Double>(new Double[] { 1.0, 0.9, 5.0 / 6.0, 0.8, 0.75, 0.7, 2.0 / 3.0, 0.6, 0.5, 0.4, 1.0 / 2.0, 0.25, 0.2, 1.0 / 6.0, 1.0 / 7.0, 0.125, 1.0 / 9.0, 0.1, 0.05, 0 });
        public IEnumerable<Double> GradeList => _gradeList;
    }
}
