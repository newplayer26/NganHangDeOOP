using NganHangDe.Models;
using NganHangDe.ModelsDb;
using NganHangDe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public  class LoadSingleQuestionCommand : AsyncCommandBase
    {
        private Action<QuestionModel, List<AnswerModel>> _loadQuestion;
        private QuestionService _service = new QuestionService();


        public LoadSingleQuestionCommand(Action<QuestionModel, List<AnswerModel>> loadQuestion)
        {
            _loadQuestion = loadQuestion;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is int Id)
            {
                try
                {
                    Question returnedQuestion = await _service.GetFullQuestionById(Id);
                    QuestionModel qModel = new QuestionModel { Id = returnedQuestion.Id , Name= returnedQuestion.Name, Text = returnedQuestion.Text, CategoryId = returnedQuestion.CategoryId };
                    List<AnswerModel> aList = new List<AnswerModel>();
                    foreach(Answer answer in returnedQuestion.Answers){
                        AnswerModel aModel = new AnswerModel { Id = answer.Id, Text = answer.Text, Grade = answer.Grade, AnswerGroup = returnedQuestion.Id };
                        aList.Add(aModel);
                    }
                    _loadQuestion(qModel, aList);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
