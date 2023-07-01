using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NganHangDe.Commands
{
    public class ImportCommand : AsyncCommandBase
    {
        private ImportTabViewModel _viewmodel;
        private Action<string> _afterImport;
        private FileService _fileservice = new FileService();
        private CategoryService _categoryService = new CategoryService();
        public ImportCommand(Action<string> afterImport, ImportTabViewModel viewmodel)
        {
            _afterImport = afterImport;
            _viewmodel = viewmodel;
            _viewmodel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            string message = string.Empty;
            FileInfo fileInfo = new FileInfo(_viewmodel.SelectedFilePath);
            long fileSizeInBytes = fileInfo.Length;
            const long maxFileSizeInBytes = 100 * 1024 * 1024; // Maximum size = 100MB
            if (fileSizeInBytes > maxFileSizeInBytes)
            {
                message = "File size exceeds the maximum limit of 100MB.";
                return;
            }
            if (await _categoryService.GetNumberofCategoriesAsync() == 0)
            {
                await _categoryService.CreateCategoryAsync(null, "18/07/2023", string.Empty);
            }
            int categoryId = await _categoryService.GetRandomCategoryIdAsync();
            message = await _fileservice.AddQuestionsByFile(_viewmodel.SelectedFilePath, categoryId);
            _afterImport(message);
        }
        public override bool CanExecute(object parameter)
        {
            return _viewmodel.CanImport && base.CanExecute(parameter);
        }
        public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ImportTabViewModel.CanImport))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
