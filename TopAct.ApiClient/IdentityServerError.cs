using System;

namespace TopAct.ApiClient
{
    public class IdentityServerException : Exception
    {
        public IdentityServerException(string message) : base(message) { }
    }
}
