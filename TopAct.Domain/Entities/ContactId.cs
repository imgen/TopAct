using System;

namespace TopAct.Domain.Entities
{
    public class ContactId : TypedIdValueBase
    {
        public ContactId(Guid value)
            : base(value)
        {
        }
    }
}
