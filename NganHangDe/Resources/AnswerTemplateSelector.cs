using NganHangDe.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NganHangDe.Resources
{
    public class AnswerTemplateSelector : DataTemplateSelector
        {
        public DataTemplate AnswerWithGradeTemplate { get; set; }
        public DataTemplate DefaultAnswerTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Answer answer && answer.Grade > 0)
            {
                return AnswerWithGradeTemplate;
            }
            return DefaultAnswerTemplate;
        }   
    }
}

