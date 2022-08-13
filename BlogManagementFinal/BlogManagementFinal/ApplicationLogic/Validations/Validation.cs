using System;
namespace BlogManagementFinal.ApplicationLogic.Validations
{
    public class Validation
    {
        public static bool IsLengthBetween(string text, int start, int end)
        {
            return text.Length >= start && text.Length <= end;
        }
    }
}