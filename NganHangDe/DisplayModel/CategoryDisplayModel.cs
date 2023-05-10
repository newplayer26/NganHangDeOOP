
using NganHangDe.ViewModels;

namespace NganHangDe.DisplayModel
{
    public class CategoryDisplayModel : ViewModelBase
    {
        private string _name;
        private string _level;
        public int Id { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        private string _displayedName;
        public string DisplayedName
        {
            get
            {
                return Level + Name;
            }
        }
    }

}
