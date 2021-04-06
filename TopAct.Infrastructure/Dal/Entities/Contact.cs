using System;
using System.Collections.Generic;
using System.Linq;

namespace TopAct.Infrastructure.Dal.Entities
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string OrganisationName { get; set; }
        public string WebsiteUrl { get; set; }
        public string Notes { get; set; }

        private IList<Phone> _phones;
        public IList<Phone> Phones
        {
            get => _phones;
            set
            {
                _phones = value;
                PhoneNumbers = value?.Select(x => x.PhoneNo).ToArray() ??
                    Array.Empty<string>();
            }
        }
        public IList<string> PhoneNumbers { get; set; }
        public IList<string> Addresses { get; set; }
        public IList<string> Emails { get; set; }
        public IList<string> Categories { get; set; }
        public IList<string> Tags { get; set; }
        public Dictionary<string, string> CustomFields { get; set; }
    }
}
