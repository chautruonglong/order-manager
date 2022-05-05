using System;

namespace AspNet.Tutorial.Core.Exceptions
{
    public class RecordNotFoundException : NullReferenceException
    {
        public RecordNotFoundException() : base("Record not found in database")
        {
        }

        public RecordNotFoundException(string message) : base(message)
        {
        }
    }
}
