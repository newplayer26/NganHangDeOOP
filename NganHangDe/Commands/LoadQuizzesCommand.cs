using NganHangDe.Models;
using NganHangDe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class LoadQuizzesCommand:AsyncCommandBase
    {
        private QuizService _service = new QuizService();
        private Action<List<QuizModel>> _loadQuizzesFunc;
        public LoadQuizzesCommand(Action<List<QuizModel>> loadQuizzesFunc)
        {
            _loadQuizzesFunc = loadQuizzesFunc;
        }
        public override async Task ExecuteAsync(object parameter)
        {
          
                try
                {
                    List<QuizModel> list = await _service.GetAllQuizzesAsync();
                    _loadQuizzesFunc(list);
                   
                }
                catch (Exception)
                {

                }
            }
        
    }
}
