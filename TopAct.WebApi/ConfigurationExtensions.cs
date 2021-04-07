using Microsoft.Extensions.Configuration;
using static TopAct.Common.SharedConstants;

namespace TopAct.WebApi
{
    public static class ConfigurationExtensions
    {
        public static string GetDbFilePath(this IConfiguration config) =>
            config.GetValue("DbFilePath", DefaultDbFilePath);
    }
}
