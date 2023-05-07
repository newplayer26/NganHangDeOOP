using NganHangDe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class QuizSelectedCommand : CommandBase
    {
        private readonly Action _quizSelected;

        public QuizSelectedCommand(Action quizSelected)
        {
            _quizSelected = quizSelected;
        }

        public override void Execute(object parameter)
        {
            _quizSelected();
        }
    }
}
