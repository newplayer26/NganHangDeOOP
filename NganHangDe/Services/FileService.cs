using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NganHangDe.Models;
using NganHangDe.ModelsDb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Category = NganHangDe.ModelsDb.Category;

namespace NganHangDe.Services
{
    public class FileService : IFileService
    {
        private ICategoryService _categoryService = new CategoryService();
        public async Task<string> AddQuestionsByFile(string filePath, int categoryId)
        {
            Category category = await _categoryService.GetFullCategoryById(categoryId);
            string fileExtension = Path.GetExtension(filePath);
            IQuestionService _service = new QuestionService();
            if (fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                List<QuestionModel> questionList = new List<QuestionModel>();
                string status = GetQuestions(File.ReadAllText(filePath), questionList, category.Name);
                if(status[0] == 'S')
                {
                    foreach(QuestionModel question in questionList)
                    {
                        Console.WriteLine(question.Text);
                        Console.WriteLine(question.Answers[0].Text);
                        Question currentQuestion = await _service.CreateQuestionAsync(question, categoryId, question.Answers);
                    }
                }
                return status;
            }
            else if (fileExtension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
            {
                List<QuestionModel> questionList = new List<QuestionModel>();
                StringBuilder sb = new StringBuilder();
                using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, false))
                {
                    DocumentFormat.OpenXml.Wordprocessing.Body body = doc.MainDocumentPart.Document.Body;
                    foreach (var paragraph in body.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
                    {
                        sb.AppendLine(paragraph.InnerText);
                    }
                }
                string docText = sb.ToString();
                string status = GetQuestions(docText, questionList, category.Name);
                if (status[0] == 'S')
                {
                    foreach (QuestionModel question in questionList)
                    {
                        Question currentQuestion = await _service.CreateQuestionAsync(question, categoryId, question.Answers);
                    }
                }
                return status;
            }
            else
            {
                return "Wrong format";
            }
        }
        string GetQuestions(string fileContent, List<QuestionModel> questionList, string categoryName) 
        {
            //Console.Write(fileContent);
            string[] lines = fileContent.Split(new[] { Environment.NewLine, "\r", "\n" }, StringSplitOptions.None);
            int cnt = 0;
            int lineNumber = 0;
            string questionText = "";
            List<AnswerModel> answerList = new List<AnswerModel>();
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                lineNumber++;
                if (cnt == -1)
                {
                    if (line == "") cnt++;
                    else return $"Error at line {lineNumber}";
                }
                else if (cnt == 0)
                {
                    if (line == "") continue;
                    questionText = line;
                    cnt++;
                }
                else if (cnt == 1 || cnt == 2)
                {
                    string checker = Convert.ToChar('A' + cnt - 1) + ". ";
                    if (line.StartsWith(checker))
                    {
                        //Console.WriteLine(line);
                        if (line.Substring(3) == "") return $"Error at line {lineNumber}";
                        answerList.Add(new AnswerModel
                        {
                            Text = line.Substring(3),
                            Grade = 0
                        });
                        cnt++;
                    }
                    else
                    {
                        //Console.WriteLine(line);
                        return $"Error at line {lineNumber}";
                    }
                }
                else
                {
                    string checker = Convert.ToChar('A' + cnt - 1) + ". ";
                    if (line.StartsWith(checker))
                    {
                        if (line.Substring(3) == "") return $"Error at line {lineNumber}";
                        answerList.Add(new AnswerModel
                        {
                            Text = line.Substring(3),
                            Grade = 0
                        });
                        cnt++;
                    }
                    else if (line.StartsWith("ANSWER: "))
                    {
                        if (line.Length == 9)
                        {
                            int answer = line[8] - 'A';
                            if (answer >= answerList.Count || answer < 0)
                            {
                                return $"Error at line {lineNumber}";
                            }
                            else
                            {
                                answerList[answer].Grade = 1;
                                cnt = -1;
                                questionList.Add(new QuestionModel
                                {
                                    Name = "",
                                    Text = questionText,
                                    Answers = answerList
                                });
                                answerList = new List<AnswerModel>();
                                questionText = "";
                            }
                        }
                        else return $"Error at line {lineNumber}";
                    }
                    else
                    {
                        return $"Error at line {lineNumber}";
                    }
                }
            }
            if (questionText == "") return $"Successfully add {questionList.Count} questions to category {categoryName} ";
            else return $"Error at line {lineNumber}";
        }
    }
}
