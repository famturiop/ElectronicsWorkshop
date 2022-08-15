// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static List<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "ElectronicsWorkshop",
                AllowedGrantTypes = new List<string> { GrantType.ClientCredentials },
                RequireConsent = false,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                Claims = new[]
                {
                    new ClientClaim("Type","Read"),
                    new ClientClaim("Type","Write"),
                    new ClientClaim("Type","Delete")
                },
                ClientClaimsPrefix = "",
                AllowedScopes = { "FullApiAccess" }
            },
            new Client
            {
                ClientId = "ElectronicsWorkshopMailApp",
                AllowedGrantTypes = new List<string> { GrantType.ClientCredentials },
                RequireConsent = false,
                ClientSecrets =
                {
                    new Secret("secret2".Sha256())
                },
                Claims = new[]
                {
                    new ClientClaim("Type","Read")
                },
                ClientClaimsPrefix = "",
                AllowedScopes = { "RestrictedApiAccess" }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
        {
            new ApiScope("FullApiAccess"),
            new ApiScope("RestrictedApiAccess")
        };
    }
}