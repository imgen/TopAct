using System;
using TopAct.Domain.Entities;

namespace TopAct.Domain.Rules
{
    public class ContactWebSiteUrlMustBeValidRule : IBusinessRule
    {
        private readonly Contact _contact;

        public ContactWebSiteUrlMustBeValidRule(Contact contact)
        {
            _contact = contact;
        }

        public string Message => "Url must be valid";

        public bool IsBroken()
        {
            return _contact.WebsiteUrl is not null &&
                !Uri.IsWellFormedUriString(_contact.WebsiteUrl,
                    UriKind.RelativeOrAbsolute
                );
        }
    }
}
