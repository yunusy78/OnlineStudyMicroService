// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;

namespace OnlineStudyIdentityServer
{
    public static class Config
    {
         public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("resource_catalog"){Scopes={"catalog_fullPermission"}},
                new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullPermission"}},
                new ApiResource("resource_cart"){Scopes={"cart_fullPermission"}},
                new ApiResource("resource_discount"){Scopes={"discount_fullPermission"}},
                new ApiResource("resource_order"){Scopes={"order_fullPermission"}},
                new ApiResource("resource_payment"){Scopes={"payment_fullPermission"}},
                new ApiResource("resource_gateway"){Scopes={"gateway_fullPermission"}},
                new ApiResource("resource_contact"){Scopes={"contact_fullPermission"}},
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
               
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                          new IdentityResources.Email(),
                          new IdentityResources.OpenId(),
                          new IdentityResources.Profile(),
                          new IdentityResource(){Name="roles",DisplayName="Roles",Description="User Roles",UserClaims=new []{"role"}}
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullPermission","Access full catalog API"),
                new ApiScope("photo_stock_fullPermission","Access full photo stock API"),
                new ApiScope("cart_fullPermission","Access full cart API"),
                new ApiScope("discount_fullPermission","Access full discount API"),
                new ApiScope("order_fullPermission","Access full order API"),
                new ApiScope("payment_fullPermission","Access full payment API"),
                new ApiScope("gateway_fullPermission","Access full gateway API"),
                new ApiScope("contact_fullPermission","Access full contact API"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "AspNetCoreMvcClient",
                    ClientName = "Client",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"catalog_fullPermission","photo_stock_fullPermission", "gateway_fullPermission",
                        IdentityServerConstants.LocalApi.ScopeName}
                },
                // interactive client using code flow + pkce
                
                new Client()
                {
                    ClientId = "AspNetCoreMvcClient1",
                    ClientName = "AspNetCoreMvcClient",
                    AllowOfflineAccess = true,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {"cart_fullPermission","order_fullPermission","gateway_fullPermission","contact_fullPermission",
                        IdentityServerConstants.LocalApi.ScopeName,IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,"roles"},
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    
                },
                
                new Client ()
                {
                    ClientId = "TokenExchangeClient",
                    ClientName = "Token Exchange Client",
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = new [] {"urn:ietf:params:oauth:grant-type:token-exchange"},
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedScopes = {"gateway_fullPermission","discount_fullPermission", "payment_fullPermission", 
                        IdentityServerConstants.StandardScopes.OpenId}
                },

                
            };
    }
}