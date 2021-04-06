namespace TopAct.Common
{
    public class SharedConstants
    {
        public const string ClientId = "client";
        public const string ApiSecret = "secret";
        public const string ApiScope = "api";
        public const string IdentityServerUrl = "https://localhost:5001";
        public const string AuthenticationSchemeName = "Bearer";
        public const string AppName = "TopAct";

        public const string DefaultDbFilePath = @"C:\db\topact.db";

        public const int PhoneNoLength = 8;

        public const string VCardContentType = "text/vcard";
        public static readonly string[] ValidVCardContentTypes = { VCardContentType, "text/x-vcard" };
        public static readonly string[] VCardFileExtensions = { ".vcard", ".vcf" };
    }
}
