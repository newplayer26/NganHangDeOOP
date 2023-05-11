
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NganHangDe.ViewModels;

namespace NganHangDe.Models
{
    public class CategoryModel : ViewModelBase
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
                OnPropertyChanged(nameof(DisplayedName));
            }
        }

        public string Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
                OnPropertyChanged(nameof(DisplayedName));
            }
        }

        public string DisplayedName
        {
            get
            {
                return Level + Name;
            }
        }
        private string _displayedName;
  
    }


}
