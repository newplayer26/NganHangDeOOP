using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.ViewModels.TabbedNavigationTabViewModels
{
    public class CategoriesTabViewModel:ViewModelBase
    {

        private AllTabsViewModel _parentViewModel;
        public ICommand SubmitCategoryCommand { get; set; }
        public ICommand LoadCategoriesCommand { get; }
        private ObservableCollection<CategoryModel> _categoryList = new ObservableCollection<CategoryModel>();
        public IEnumerable<CategoryModel> CategoryList => _categoryList;
        private CategoryModel _selectedCategory = null;
        private string _categoryName = string.Empty;
        private string _categoryInfo = string.Empty;
        private string _idNumber;
        private readonly NavigationStore _ancestorNavigationStore;
        public CategoriesTabViewModel(AllTabsViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
            _ancestorNavigationStore = parentViewModel.AncestorNavigationStore;
            LoadCategoriesCommand = new GetCategoriesCommand(LoadCategories);
            LoadCategoriesCommand.Execute(null);
            AfterCreate = _afterCreate;
            SubmitCategoryCommand = new SubmitCategoryCommand(this, AfterCreate);
        }
        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                _categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
                OnPropertyChanged(nameof(CanCreateCategory));
            }
        }
        public string CategoryInfo
        {
            get
            {
                return _categoryInfo;
            }
            set
            {
                _categoryInfo = value;
                OnPropertyChanged(nameof(CategoryInfo));
            }
        }
        public string IdNumber
        {
            get
            {
                return _idNumber;
            }
            set
            {
                _idNumber = value;
                OnPropertyChanged(nameof(IdNumber));
            }    
        }
        public bool CanCreateCategory => !string.IsNullOrEmpty(CategoryName);
        public void LoadCategories (List<CategoryModel> list)
        {
            _categoryList.Clear();
            foreach (var category in list)
            {
                _categoryList.Add(category);
            }
            OnPropertyChanged(nameof(CategoryList));
        }
        private void _afterCreate()
        {
            SelectedCategory = null;
            CategoryName = string.Empty;
            CategoryInfo = string.Empty;
            IdNumber = string.Empty;
            LoadCategoriesCommand.Execute(null);
            _parentViewModel.QuestionsTabViewModel.LoadCategoriesCommand.Execute(null);
        }
        public Action AfterCreate { get; set; }
    }
}
