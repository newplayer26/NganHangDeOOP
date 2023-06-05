using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Services
{
    public interface IFileService
    {
        Task<string> AddQuestionsByFile(string filePath, int categoryId);
    }
}
