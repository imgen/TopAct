using System;
using TopAct.Domain.Entities;
using TopAct.Common;
using TopAct.Domain.Contracts;

namespace TopAct.Domain.Rules
{
    public class EmailMustBeValidRule : IBusinessRule
    {
        private readonly Email _email;

        public EmailMustBeValidRule(Email email)
        {
            _email = email;
        }

        public string Message => $"The email {_email.EmailAddress} is not valid";

        public bool IsBroken()
        {
            return _email.EmailAddress is not null &&
                !_email.EmailAddress.IsValidEmailAddress();
        }
    }
}
