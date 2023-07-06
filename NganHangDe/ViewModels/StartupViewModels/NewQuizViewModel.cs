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
using System.Windows.Input;

namespace NganHangDe.ViewModels.StartupViewModels
{
    public class NewQuizViewModel : ViewModelBase
    {
        private string _name, _description, _time;
        private string _selectedTimeForm;
        private ObservableCollection<string> _timeForms = new ObservableCollection<string>(new string[] { "Minutes", "Hours" });
        public ObservableCollection<string> TimeForms
        {
            get { return _timeForms; }
            set
            {
                _timeForms = value;
                OnPropertyChanged(nameof(TimeForms));
            }
        }
        public string Name { 
            get {
                return _name;
            } 
            set { 
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(CanCreateQuiz));
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public string SelectedTimeForm
        {
            get
            {
                return _selectedTimeForm;
            }
            set
            {
                _selectedTimeForm = value;
                OnPropertyChanged(nameof(SelectedTimeForm));
            }
        }
        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
                OnPropertyChanged(nameof(CanCreateQuiz));
            }
        }
        private bool _isTimeEnabled;
        public bool IsTimeEnabled
        {
            get { return _isTimeEnabled; }
            set
            {
                _isTimeEnabled = value;
                if (!_isTimeEnabled)
                {
                    Time = "1";
                    SelectedTimeForm = TimeForms[1];
                }
                OnPropertyChanged(nameof(IsTimeEnabled));
            }
        }
        public bool CanCreateQuiz => !StringExtensions.IsNullOrEmptyOrWhiteSpace(Name) && int.TryParse(Time, out _) && int.Parse(Time) > 0;
        public ICommand CreateQuizCommand { get; }

        private readonly NavigationStore _ancestorNavigationStore;
        public ICommand ToAllQuizzesViewCommand { get;  }
        public NewQuizViewModel(NavigationStore ancestorNavigationStore)
        {
            _ancestorNavigationStore = ancestorNavigationStore;
            ToAllQuizzesViewCommand = new NavigateCommand<AllQuizzesViewModel>(_ancestorNavigationStore, typeof(AllQuizzesViewModel));
            CreateQuizCommand = new CreateQuizCommand(this);
            SelectedTimeForm = TimeForms[1];
            Name = string.Empty;
            Description = string.Empty;
            Time = "1";
        }
    }
}
