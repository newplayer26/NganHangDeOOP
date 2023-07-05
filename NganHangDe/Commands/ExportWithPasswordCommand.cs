using NganHangDe.Models;
using NganHangDe.ModelsDb;
using NganHangDe.Services;
using NganHangDe.ViewModels.StartupViewModels;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NganHangDe.Commands
{
    public class ExportWithPasswordCommand : AsyncCommandBase
    {
        private QuizPageViewModel _viewmodel;
        private FileService _fileService = new FileService();
        private QuizService _quizService = new QuizService();
        public ExportWithPasswordCommand(QuizPageViewModel viewmodel)
        {
            _viewmodel = viewmodel;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            List<QuestionModel> questionmodels = new List<QuestionModel>();
            if (_viewmodel.IsShuffleChecked == false)
            {
                questionmodels = await _quizService.GetAllQuestionsFromQuizAsync(_viewmodel.Quiz.Id);
            }
            else questionmodels = _viewmodel.ShuffledQuestionList.ToList();
            var pdfData = _fileService.GeneratePdf(questionmodels, _viewmodel.Password);
            _fileService.SavePdfFile(pdfData);
        }
    }
}
