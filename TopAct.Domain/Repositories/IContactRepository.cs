using TopAct.Domain.Entities;

namespace TopAct.Domain.Repositories
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        Contact GetById(ContactId contactId);
    }
}
