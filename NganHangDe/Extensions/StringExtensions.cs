using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(string value)
        {
            return string.IsNullOrEmpty(value) || value.All(char.IsWhiteSpace);
        }
    }
}
