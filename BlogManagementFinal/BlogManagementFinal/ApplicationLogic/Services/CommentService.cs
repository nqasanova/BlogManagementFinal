using System;
using BlogManagementFinal.ApplicationLogic.Validations;

namespace BlogManagementFinal.ApplicationLogic.Services
{
    public class CommentService
    {
        public static string GetComment()
        {
            bool isExceptionValid;
            string comment = null;

            do
            {
                try
                {
                    Console.Write("Enter title : ");
                    comment = Console.ReadLine();

                    if (comment == "null")
                    {
                        throw new Exception();
                    }

                    isExceptionValid = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("Something went wrong...");
                    isExceptionValid = true;
                }

            } while (isExceptionValid || !Validation.IsLengthBetween(comment, 10, 35));

            return comment;
        }
    }
}
