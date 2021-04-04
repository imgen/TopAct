using System.Linq;
using TopAct.Domain.Contracts;
using TopAct.Domain.Entities;

namespace TopAct.Domain.Rules
{
    public class PhoneNoMustBeOf8DigitsRule : IBusinessRule
    {
        private readonly Phone _phone;

        public PhoneNoMustBeOf8DigitsRule(Phone phone)
        {
            _phone = phone;
        }
        public string Message => "The phone number must be of 8 digits";

        public bool IsBroken()
        {
            return string.IsNullOrWhiteSpace(_phone.PhoneNo) ||
                _phone.PhoneNo.Length != 8 ||
                !_phone.PhoneNo.All(char.IsDigit);
        }
    }
}
