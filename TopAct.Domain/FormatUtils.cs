using TopAct.Common;
using TopAct.Domain.Rules;

namespace TopAct.Domain
{
    public static class FormatUtils
    {
        public static string FormatPhone(this string s)
        {
            const int HalfLength = SharedConstants.PhoneNoLength / 2;
            return s.IsValidPhoneNo() ? s[0..HalfLength] + "-" + s[HalfLength..] : s;
        }
    }
}
