using System.Linq;
using System.Text.RegularExpressions;
using TopAct.Common;

namespace TopAct.Domain.Rules
{
    public static class RuleUtils
    {
        public static bool IsValidEmailAddress(this string s)
        {
            var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            return regex.IsMatch(s);
        }

        public static bool IsValidPhoneNo(this string phone)
        {
            return !phone.IsNullOrWhiteSpace() &&
                phone.Length == 8 &&
                phone.All(char.IsDigit);
        }
    }
}
