using System;

namespace TopAct.Domain.Exceptions
{
    public class ContactNotFoundException : Exception
    {
        public ContactNotFoundException(string message) : base(message)
        { }
    }
}
