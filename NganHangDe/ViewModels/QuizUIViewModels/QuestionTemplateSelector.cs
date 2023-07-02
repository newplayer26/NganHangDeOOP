using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NganHangDe.Models;
using NganHangDe.ModelsDb;
using NganHangDe.Services;
using NganHangDe.Stores;

namespace NganHangDe.ViewModels.QuizUIViewModels
{
    class QuestionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SingleAnswerTemplate { get; set; }
        public DataTemplate MultipleAnswerTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            QuestionModel question = item as QuestionModel;

            if (question != null)
            {
                if (!question.IsMultipleAnswers)
                {
                    return SingleAnswerTemplate;

                }
                else if (question.IsMultipleAnswers)
                {
                    return MultipleAnswerTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
