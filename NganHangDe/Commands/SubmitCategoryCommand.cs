using NganHangDe.Models;
using NganHangDe.ModelsDb;
using NganHangDe.Services;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class SubmitCategoryCommand : AsyncCommandBase
    {
        private CategoriesTabViewModel _viewModel;
        private readonly CategoryService _service = new CategoryService();
        private readonly Action _afterCreateAction;
        public SubmitCategoryCommand(CategoriesTabViewModel viewModel, Action afterCreateAction)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
            _afterCreateAction = afterCreateAction;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                int? parentCategoryId = null;
                if (_viewModel.SelectedCategory != null) parentCategoryId = _viewModel.SelectedCategory.Id;
                await _service.CreateCategoryAsync(parentCategoryId, _viewModel.CategoryName, _viewModel.CategoryInfo);
                _afterCreateAction?.Invoke();
            }
            catch (Exception ex)
            {

            }
        }
        public override bool CanExecute(object parameter)
        {
            return _viewModel.CanCreateCategory && base.CanExecute(parameter);
        }
        public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CategoriesTabViewModel.CanCreateCategory))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
