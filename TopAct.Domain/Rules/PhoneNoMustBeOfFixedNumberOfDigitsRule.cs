using TopAct.Common;
using TopAct.Domain.Contracts;
using TopAct.Domain.Entities;

namespace TopAct.Domain.Rules
{
    public class PhoneNoMustBeOfFixedNumberOfDigitsRule : IBusinessRule
    {
        private readonly Phone _phone;

        public PhoneNoMustBeOfFixedNumberOfDigitsRule(Phone phone)
        {
            _phone = phone;
        }

        public string Message => $"The phone number must be of {SharedConstants.PhoneNoLength} digits";

        public bool IsBroken()
        {
            return string.IsNullOrWhiteSpace(_phone.PhoneNo) is false &&
                _phone.PhoneNo.IsValidPhoneNo() is false;
        }
    }
}
