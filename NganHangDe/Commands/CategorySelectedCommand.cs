using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class CategorySelectedCommand : CommandBase
    {
        private readonly Action _categorySelected;

        public CategorySelectedCommand(Action categorySelected)
        {
            _categorySelected = categorySelected;
        }

        public override void Execute(object parameter)
        {
            _categorySelected();
        }
    }
}
