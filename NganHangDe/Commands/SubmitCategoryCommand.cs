using NganHangDe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class SubmitCategoryCommand : AsyncCommandBase
    {
        private readonly CategoryService _service = new CategoryService();
        public SubmitCategoryCommand()
        {
            
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}
