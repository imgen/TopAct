// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using TopAct.Common;
using static TopAct.Common.SharedConstants;

namespace TopAct.WebApi
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(SharedConstants.ApiScope, "TopAct")
            };


        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = ClientId,

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret(ApiSecret.Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = {
                        SharedConstants.ApiScope,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },

                    RedirectUris = new []
                    {
                        "https://localhost:5001/swagger/oauth2-redirect.html"
                    },
                    RequirePkce = true
                }
            };
    }
}