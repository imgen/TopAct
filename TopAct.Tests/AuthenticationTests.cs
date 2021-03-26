using FluentAssertions;
using System.Threading.Tasks;
using TopAct.ApiClient;
using Xunit;
using static TopAct.Common.SharedConstants;

namespace TopAct.Tests
{
    public class AuthenticationTests
    {
        [Fact]
        public async Task TestJwtBearer()
        {
            var client = await TopActApiClient.BuildHttpClient(IdentityServerUrl,
                ClientId,
                ApiSecret,
                ApiScope
            );
            var response = await client.GetStringAsync("https://localhost:5001/Contact");
            response.Should().NotBeNullOrWhiteSpace();
        }
    }
}
