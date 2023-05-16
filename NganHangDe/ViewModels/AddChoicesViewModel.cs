using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using NganHangDe.Commands;

namespace NganHangDe.ViewModels
{
    public class AddChoicesViewModel : ViewModelBase
    {
        private ObservableCollection<StackPanelViewModel> _containers = new ObservableCollection<StackPanelViewModel>();
        private int _counter = 3;
        public ObservableCollection<StackPanelViewModel> Choices
        {
            get { return _containers; }
            set
            {
                _containers = value;
                OnPropertyChanged(nameof(Choices));
            }
        }
        public ICommand AddChoicesCommand { get; }

        public AddChoicesViewModel()
        {
            _containers = new ObservableCollection<StackPanelViewModel>();
            AddChoicesCommand = new RelayCommand(AddChoices);
        }
        int i = 3;
        private void AddChoices(object parameter)
        {   
            for(int i=1; i<=3; i++)
                _containers.Add(new StackPanelViewModel { Number = "Choice " + _counter++.ToString()});
        }
    }
    public class StackPanelViewModel
    {
        public string Text { get; set; }
        public string Number { get; set; }
        
       
    }
}
