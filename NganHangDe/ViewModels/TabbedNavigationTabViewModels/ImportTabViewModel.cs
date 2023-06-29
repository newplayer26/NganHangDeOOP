using Microsoft.Win32;
using NganHangDe.Commands;
using System;
using System.Collections.Generic;
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
        private string selectedFilePath;
        private string fileName;
        
     
 

        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }
        public ICommand FileButtonCommand { get;  }
        public ICommand ImportButtonCommand { get;  }
        
        public ICommand DropCommand { get; } 
        public ImportTabViewModel()
        {
            FileButtonCommand = new RelayCommand(FileButton_Click);
            ImportButtonCommand = new RelayCommand(ImportButton_Click);
            
            DropCommand = new RelayCommand(Drop);

        }
        private void FileButton_Click(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|Word Documents (*.docx)|*.docx";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
                FileName = Path.GetFileName(selectedFilePath);
            }
        }
        private void CheckFileFormatAndShowMessageBox()
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please select a file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string fileExtension = Path.GetExtension(selectedFilePath);
            if (fileExtension.ToLower() != ".txt" && fileExtension.ToLower() != ".docx")
            {
                MessageBox.Show("Wrong format. Only .txt and .docx files are allowed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            FileInfo fileInfo = new FileInfo(selectedFilePath);
            long fileSizeInBytes = fileInfo.Length;
            const long maxFileSizeInBytes = 100 * 1024 * 1024; // Maximum size = 100MB

            if (fileSizeInBytes > maxFileSizeInBytes)
            {
                MessageBox.Show("File size exceeds the maximum limit of 100MB.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("OK.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        selectedFilePath = filePath; 
                        FileName = System.IO.Path.GetFileName(selectedFilePath); 
                        
                        
                    }
                }
            }
        }
        private void ImportButton_Click(object parameter)
        {
            CheckFileFormatAndShowMessageBox();
        }



    }
}
