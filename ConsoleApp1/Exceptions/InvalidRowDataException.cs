using System;

namespace ConsoleApp1.Exceptions
{
   public class InvalidRowDataException : Exception
    {
        public int Row { get; private set; }
        public InvalidRowDataException(string message, int row ) : base(message)
        {
            Row = row;            
        }
    }
}
