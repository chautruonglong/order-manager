using System;

namespace AspNet.Tutorial.Core.Exceptions
{
    public class PaginationException : SystemException
    {
        public PaginationException() : base("Pagination is not accepted")
        {
        }

        public PaginationException(string message) : base(message)
        {
        }
    }
}
