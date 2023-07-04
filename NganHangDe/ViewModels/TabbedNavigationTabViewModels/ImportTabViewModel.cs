using Microsoft.Win32;
using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NganHangDe.ViewModels.TabbedNavigationTabViewModels
{
    public class ImportTabViewModel : ViewModelBase
    {
        private AllTabsViewModel _parentViewModel;
        private readonly NavigationStore _ancestorNavigationStore;
        private string selectedFilePath = string.Empty;
        public string SelectedFilePath
        {
            get { return selectedFilePath; }
            set
            {
                selectedFilePath = value;
                OnPropertyChanged(nameof(SelectedFilePath));
            }
        }
        private string fileName = string.Empty;
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                OnPropertyChanged(nameof(FileName));
                OnPropertyChanged(nameof(CanImport));
            }
        }
        public ICommand FileButtonCommand { get;  }
        public ICommand ImportCommand { get;  }
        
        public ICommand DropCommand { get; }
        public bool CanImport => !string.IsNullOrEmpty(FileName);
        public ImportTabViewModel(AllTabsViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
            _ancestorNavigationStore = parentViewModel.AncestorNavigationStore;
            FileButtonCommand = new RelayCommand(FileButton_Click);
            DropCommand = new RelayCommand(Drop);
            AfterImport = afterImport;
            ImportCommand = new ImportCommand(AfterImport, this);

        }
        public Action<string> AfterImport { get; set; }
        private void afterImport(string message)
        {
            MessageBox.Show(message);
            FileName = string.Empty;
            SelectedFilePath = string.Empty;
            _parentViewModel.QuestionsTabViewModel.LoadCategoriesCommand.Execute(null);
            _parentViewModel.CategoriesTabViewModel.LoadCategoriesCommand.Execute(null);
        }
        private void FileButton_Click(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Text Files (*.txt)|*.txt|Word Documents (*.docx)|*.docx";
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFilePath = openFileDialog.FileName;
                FileName = Path.GetFileName(SelectedFilePath);
            }
        }
        private void Drop(object parameter)
        {
            if (parameter is DragEventArgs e)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (files != null && files.Length > 0)
                    {
                        string filePath = files[0]; 
                        SelectedFilePath = filePath; 
                        FileName = System.IO.Path.GetFileName(SelectedFilePath); 
                    }
                }
            }
        }

    }
}
