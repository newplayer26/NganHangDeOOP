using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class QuestionSelectedCommand : CommandBase
    {
        private readonly Action _questionSelected;

        public QuestionSelectedCommand(Action questionSelected)
        {
            _questionSelected = questionSelected;
        }

        public override void Execute(object parameter)
        {
            _questionSelected();
        }
    }
}
