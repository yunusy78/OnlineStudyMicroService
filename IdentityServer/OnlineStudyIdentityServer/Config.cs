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
                new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
                new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
                new ApiResource("resource_cart"){Scopes={"cart_fullpermission"}},
                new ApiResource("resource_discount"){Scopes={"discount_fullpermission"}},
                new ApiResource("resource_order"){Scopes={"order_fullpermission"}},
                new ApiResource("resource_payment"){Scopes={"payment_fullpermission"}},
                new ApiResource("resource_gateway"){Scopes={"gateway_fullpermission"}},
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
                new ApiScope("catalog_fullpermission","Access full catalog API"),
                new ApiScope("photo_stock_fullpermission","Access full photo stock API"),
                new ApiScope("cart_fullpermission","Access full cart API"),
                new ApiScope("discount_fullpermission","Access full discount API"),
                new ApiScope("order_fullpermission","Access full order API"),
                new ApiScope("payment_fullpermission","Access full payment API"),
                new ApiScope("gateway_fullpermission","Access full gateway API"),
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
                    AllowedScopes = {"catalog_fullpermission","photo_stock_fullpermission", "gateway_fullpermission",
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
                    AllowedScopes = {"cart_fullpermission","order_fullpermission","gateway_fullpermission",
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
                    AllowedScopes = {"gateway_fullpermission","discount_fullpermission", "payment_fullpermission", 
                        IdentityServerConstants.StandardScopes.OpenId}
                },

                
            };
    }
}