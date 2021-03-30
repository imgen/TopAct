using System.Threading.Tasks;
using TopAct.Domain.Entities;

namespace TopAct.Domain
{
    public interface IMeetingRepository
    {
        Task AddAsync(Contact contact);
        Task<Contact> GetByIdAsync(ContactId contactId);
    }
}
