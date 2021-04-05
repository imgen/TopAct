using System;

namespace TopAct.Domain.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message) : base(message)
        { }
    }
}
