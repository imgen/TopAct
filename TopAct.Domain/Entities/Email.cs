using TopAct.Domain.Rules;

namespace TopAct.Domain.Entities
{
    public class Email : Entity
    {
        public string EmailAddress { get; private set; }

        public Email(string emailAddress)
        {
            EmailAddress = emailAddress;
            CheckRule(new EmailMustBeValidRule(this));
        }
    }
}
