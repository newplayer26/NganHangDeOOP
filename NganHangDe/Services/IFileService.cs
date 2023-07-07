using NganHangDe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Services
{
    public interface IFileService
    {
        byte[] GeneratePdf(List<QuestionModel> questions);
        byte[] GeneratePdf(List<QuestionModel> questions, string password);
        void SavePdfFile(byte[] pdfData);
        Task<string> AddQuestionsByFile(string filePath, int categoryId);
    }
}
