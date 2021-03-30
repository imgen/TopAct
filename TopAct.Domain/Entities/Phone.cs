using TopAct.Domain.Rules;

namespace TopAct.Domain.Entities
{
    public class Phone : Entity
    {
        public string PhoneNo { get; private set; }

        public Phone(string phoneNo)
        {
            PhoneNo = phoneNo;
            CheckRule(new PhoneNoMustBeOf8DigitsRule(this));
        }
    }
}
