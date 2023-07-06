using NganHangDe.Commands;
using NganHangDe.ModelsDb;
using NganHangDe.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NganHangDe.Models
{
    public class QuestionModel : ViewModelBase
    {
        private string _name;
        private string _text;
        private List<AnswerModel> _answers;
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(DisplayedText));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayedText));
            }
        }
        public List<AnswerModel> Answers
        {
            get => _answers;
            set
            {
                _answers = value;
                OnPropertyChanged(nameof(Answers));
            }
        }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        private int _questionNumber;
        public int QuestionNumber
        {
            get { return _questionNumber; }
            set
            {
                _questionNumber = value;
                OnPropertyChanged(nameof(QuestionNumber));
            }
        }
        private ObservableCollection<AnswerModel> _selectedCorrectAnswers;
        public ObservableCollection<AnswerModel> SelectedCorrectAnswers
        {
            get { return _selectedCorrectAnswers; }
            set
            {
                _selectedCorrectAnswers = value;
                OnPropertyChanged(nameof(SelectedCorrectAnswers));
            }
        }
        private ObservableCollection<AnswerModel> _correctAnswers;
        public ObservableCollection<AnswerModel> CorrectAnswers
        {
            get { return _correctAnswers; }
            set
            {
                _correctAnswers = value;
                OnPropertyChanged(nameof(CorrectAnswers));
            }
        }
        private bool _isMultipleAnswers;
        public bool IsMultipleAnswers
        {
            get
            {
                int count = 0;
                foreach (var answer in _answers)
                {
                    if (answer.Grade != 0) count++;
                }
                if (count == 1) return false;
                return true;
            }
        }
        private List<QuizQuestion> _quizQuestions;
        public List<QuizQuestion> QuizQuestions
        {
            get { return _quizQuestions; }
            set
            {
                _quizQuestions = value;
                OnPropertyChanged(nameof(QuizQuestions));
            }
        }
        public string DisplayedText => $"{Name} {Text}"; 
    }
}
