using System;

namespace Project.Exceptions
{
    public class UnauthorizedResourceException : Exception
    {
        public UnauthorizedResourceException()
        {
        }

        public UnauthorizedResourceException(string message)
        {
        }

        public UnauthorizedResourceException(string message, Exception inner) 
            : base(message, inner)
        {
        }
    }
}