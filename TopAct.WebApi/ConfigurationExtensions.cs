using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TopAct.Common.SharedConstants;

namespace TopAct.WebApi
{
    public static class ConfigurationExtensions
    {
        public static string GetDbFilePath(this IConfiguration config) =>
            config.GetValue("DbFilePath", DefaultDbFilePath);
    }
}
