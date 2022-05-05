using System;

namespace AspNet.Tutorial.Core.Exceptions
{
    public class SortTypeException : SystemException
    {
        public SortTypeException() : base("Sorting type is not accepted")
        {
        }

        public SortTypeException(string message) : base(message)
        {
        }
    }
}
