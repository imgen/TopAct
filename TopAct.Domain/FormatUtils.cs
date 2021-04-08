using MixERP.Net.VCards.Models;
using TopAct.Common;
using TopAct.Domain.Rules;

namespace TopAct.Domain
{
    public static class FormatUtils
    {
        public static string FormatPhoneNo(this string s)
        {
            const int HalfLength = SharedConstants.PhoneNoLength / 2;
            return s.IsValidPhoneNo() ?
                s[0..HalfLength] + "-" + s[HalfLength..] :
                s;
        }

        public static string FormatAddress(this Address address)
        {
            return $"{address.Street}, {address.Region}, {address.Country}, {address.PostalCode}";
        }
    }
}
