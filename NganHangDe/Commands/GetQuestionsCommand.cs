using NganHangDe.Models;
using NganHangDe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class GetQuestionCommand : AsyncCommandBase
    {
        private Action<List<QuestionModel>, List<QuestionModel>> _loadQuestions;
        private QuestionService _service = new QuestionService();


        public GetQuestionCommand(Action<List<QuestionModel>, List<QuestionModel>> loadQuestions)
        {
            //_collection = collection;
            _loadQuestions = loadQuestions;   
    
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is int Id)
            {
                try
                {

                    List<QuestionModel> singleCategoryList = await _service.GetQuestionsByCategoryIdAsync(Id);
                    List<QuestionModel> descendantsCategoriesList = await _service.GetSubcategoriesQuestionsByCategoryIdAsync(Id);
                    _loadQuestions(singleCategoryList, descendantsCategoriesList);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
