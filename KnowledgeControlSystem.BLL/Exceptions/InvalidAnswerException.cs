using System;

namespace KnowledgeControlSystem.BLL.Exceptions
{
    public class InvalidAnswerException : Exception
    {
        public InvalidAnswerException(string message): base(message)
        {
        }
    }
}