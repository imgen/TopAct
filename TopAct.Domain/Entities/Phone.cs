using TopAct.Domain.Rules;

namespace TopAct.Domain.Entities
{
    public class Phone : Entity
    {
        public string PhoneNo { get; private set; }
        public PhoneType Type { get; private set; }

        public Phone(string phoneNo, PhoneType type)
        {
            PhoneNo = phoneNo;
            Type = type;
            CheckRule(new PhoneNoMustBeOfFixedNumberOfDigitsRule(this));
        }
    }
}
