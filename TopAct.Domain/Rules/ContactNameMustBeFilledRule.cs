using TopAct.Domain.Entities;

namespace TopAct.Domain.Rules
{
    public class ContactNameMustBeFilledRule : IBusinessRule
    {
        private readonly Contact _contact;

        public ContactNameMustBeFilledRule(Contact contact)
        {
            _contact = contact;
        }

        public string Message => "The contact's first name or last name is missing";

        public bool IsBroken()
        {
            return string.IsNullOrWhiteSpace(_contact.FirstName) ||
                string.IsNullOrWhiteSpace(_contact.LastName);
        }
    }
}
