using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.ViewModels;
using NganHangDe.ViewModels.StartupViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class GetUnassignedQuestionsCommand : AsyncCommandBase
    {
        private Action<List<QuestionModel>, List<QuestionModel>> _loadQuestions;
        private QuestionService _service = new QuestionService();
        private int _quizId;

        public GetUnassignedQuestionsCommand(Action<List<QuestionModel>, List<QuestionModel>> loadQuestions, int quizId)
        {
            //_collection = collection;
            _loadQuestions = loadQuestions;
            _quizId = quizId;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if(parameter is int Id)
            {
                try
                {
                    List<QuestionModel> singleCategoryList = await _service.GetUnassignedQuestionsAsync(_quizId, Id);
                    List<QuestionModel> descendantsCategoriesList = await _service.GetUnassignedSubcategoriesQuestionsAsync(_quizId, Id);
                    _loadQuestions(singleCategoryList, descendantsCategoriesList);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
