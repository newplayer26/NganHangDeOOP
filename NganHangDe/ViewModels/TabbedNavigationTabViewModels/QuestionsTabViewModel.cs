using NganHangDe.Commands;
using NganHangDe.DisplayModel;
using NganHangDe.Services;
using NganHangDe.Stores;
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
        private ObservableCollection<CategoryDisplayModel> _categoryList = new ObservableCollection<CategoryDisplayModel>();
        public IEnumerable<CategoryDisplayModel> CategoryList => _categoryList;
        public ICommand ToNewQuestionViewCommand { get; }
        public ICommand LoadCategoriesCommand { get; }
        private CategoryDisplayModel _selectedCategory;

      
        public CategoryDisplayModel SelectedCategory
        {
            get { return _selectedCategory; }
            set {
                _selectedCategory = value; 
                OnPropertyChanged(nameof(SelectedCategory)); 
                Console.WriteLine(_selectedCategory.Id);
            }
        }

        public QuestionsTabViewModel(NavigationStore ancestorNavigationStore)
        {
           
            ToNewQuestionViewCommand = new NavigateCommand<NewQuestionViewModel>(ancestorNavigationStore, typeof(NewQuestionViewModel));
            LoadCategoriesCommand = new GetCategoriesCommand(LoadCategories);
        }
     
        public void LoadCategories(List<CategoryDisplayModel> list)
        {

            foreach (var category in list) {
                _categoryList.Add(category);
            }

        }
        public static QuestionsTabViewModel LoadViewModel(NavigationStore ancestorNavigationStore)
        {
            
            QuestionsTabViewModel viewModel = new QuestionsTabViewModel( ancestorNavigationStore);
            viewModel.LoadCategoriesCommand.Execute(null);
           
            
            return viewModel;
        }
       
    }  
}
