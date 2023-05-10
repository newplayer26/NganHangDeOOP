﻿using NganHangDe.Models;
using NganHangDe.ViewModels.StartUpViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Services
{
    public interface IQuestionService
    {
        Task<List<QuestionViewModel>> GetQuestionsByCategoryIdAsync(int categoryId);
        Task<List<QuestionViewModel>> GetSubcategoriesQuestionsByCategoryIdAsync(int categoryId);
    }
}