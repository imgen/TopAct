using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Syncfusion.Licensing;

namespace TopAct.Blazor
{
    public class Program
    {
        private const string SyncfusionLicense = "NDIyOTUyQDMxMzkyZTMxMmUzMEx1anhCeWNjbEpVOGEzUWtSNm82UG85aFFHeENjTXZUNkxSdWN6WHY1Mzg9;NDIyOTUzQDMxMzkyZTMxMmUzMGdnczhuNmVDZkN2R3U5RkFFMG1zOTVjWWQ1S1ZRdCs3Tk1xcDBhdWp6K0k9;NDIyOTU0QDMxMzkyZTMxMmUzMEJxaWRkLzNNRW1DOEFSNFZmMTBnK285RjlKVk9hZjJNVHZCM3lxNU5SOEU9;NDIyOTU1QDMxMzkyZTMxMmUzMEJiNnM0aFVsb2tqbUtkN20rQkprZ3l1bUR4OGh5UmF5RjJhSFpEWUkwQzQ9;NDIyOTU2QDMxMzkyZTMxMmUzMGN5YXVHclRVTzdlbGx0TFA1RW1hZ2hDTklyTlBzdUI2ZzZiVnh6S1NaZEk9;NDIyOTU3QDMxMzkyZTMxMmUzMEpocWVsRGNIc2ZDSDJjVVFYbTNTVmN5eWRBWkhQYVRJNzI5dTlqMlhvZlE9;NDIyOTU4QDMxMzkyZTMxMmUzMFhnVWFRWWhOOTI0WGtvSk9heUQ4c3oyS00wMEpJU2R3cGpJSDBwbWFGcnM9;NDIyOTU5QDMxMzkyZTMxMmUzMEI3ZHk1Q0xXVm5uazJTb09nUlRoODNQbU4yUTc3SEJuMHVYWVhlV1JYM1E9;NDIyOTYwQDMxMzkyZTMxMmUzMEVUbUtON28rWElUMy9FMUhsaGdNY2dUNDhCdXBvNkgvM3ZtZUdWVmtwcFk9;NDIyOTYxQDMxMzkyZTMxMmUzME11VzhqTU5kQ2hFbmZJenA2QU9wSWdVQ2kxeXFyTXFWU1JIRTJmZ2VUYWM9";
        public static void Main(string[] args)
        {
            SyncfusionLicenseProvider.RegisterLicense(SyncfusionLicense);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
