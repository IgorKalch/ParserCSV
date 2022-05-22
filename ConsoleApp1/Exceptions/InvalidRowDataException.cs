using System;

namespace CompanyTree.Exceptions
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
