using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace az_kviz.Services.Validation
{
    public static class InputValidationService
    {
        public static bool IsAnswerValid(string input, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                errorMessage = "Answer cannot be empty!";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }
    }
}