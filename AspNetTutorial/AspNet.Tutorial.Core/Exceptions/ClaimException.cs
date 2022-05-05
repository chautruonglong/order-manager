using System;

namespace AspNet.Tutorial.Core.Exceptions
{
    public class ClaimException : SystemException
    {
        public ClaimException() : base("Claim user not found")
        {
        }

        public ClaimException(string message) : base(message)
        {
        }
    }
}
