using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;

namespace TopAct.ApiClient
{
    public class TopActApiClient
    {
        public static async Task<HttpClient> BuildHttpClient(
            string identityServerUrl,
            string clientId,
            string apiSecret,
            string scope
            )
        {
            var client = new HttpClient();
            var discoveryResponse = await client.GetDiscoveryDocumentAsync(identityServerUrl);
            if (discoveryResponse.IsError)
            {
                throw new IdentityServerException(discoveryResponse.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                    new ClientCredentialsTokenRequest
                    {
                        Address = discoveryResponse.TokenEndpoint,

                        ClientId = clientId,
                        ClientSecret = apiSecret,
                        Scope = scope
                    }
                );

            if (tokenResponse.IsError)
            {
                throw new IdentityServerException(tokenResponse.Error);
            }

            client.SetBearerToken(tokenResponse.AccessToken);
            return client;
        }
    }
}
