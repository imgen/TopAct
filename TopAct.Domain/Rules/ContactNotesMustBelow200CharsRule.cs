using TopAct.Domain.Contracts;
using TopAct.Domain.Entities;

namespace TopAct.Domain.Rules
{
    public class ContactNotesMustBelow200CharsRule : IBusinessRule
    {
        private readonly Contact _contact;

        public ContactNotesMustBelow200CharsRule(Contact contact)
        {
            _contact = contact;
        }

        public string Message => $"The notes cannot be longer than 200 characters";

        public bool IsBroken()
        {
            return _contact.Notes != null && _contact.Notes.Length > 200;
        }
    }
}
