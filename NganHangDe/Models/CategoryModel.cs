<<<<<<<< HEAD:NganHangDe/DisplayModel/CategoryDisplayModel.cs
﻿
using NganHangDe.ViewModels;

namespace NganHangDe.DisplayModel
{
    public class CategoryDisplayModel : ViewModelBase
    {
        private string _name;
        private string _level;
========
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

>>>>>>>> 2d1f94b (Adjust files'name + GUI1.2 demo):NganHangDe/Models/CategoryModel.cs
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
<<<<<<<< HEAD:NganHangDe/DisplayModel/CategoryDisplayModel.cs
========

>>>>>>>> 2d1f94b (Adjust files'name + GUI1.2 demo):NganHangDe/Models/CategoryModel.cs
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
        public string DisplayedName
        {
            get
            {
                return Level + Name;
            }
        }
    }


}
