﻿using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using MSCourse.Web.Models.SettingModels;
using MSCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSCourse.Web.Services
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly ClientSettings _clientSettings;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;

        public ClientCredentialTokenService(HttpClient httpClient, IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, IClientAccessTokenCache clientAccessTokenCache)
        {
            _httpClient = httpClient;
            _serviceApiSettings = serviceApiSettings.Value;
            _clientSettings = clientSettings.Value;
            _clientAccessTokenCache = clientAccessTokenCache;
        }

        public async Task<string> GetToken()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken");

            if (currentToken != null)
            {
                return currentToken.AccessToken;
            }

            var discovery = await _httpClient.GetDiscoveryDocumentAsync(
              new DiscoveryDocumentRequest
              {
                  Address = _serviceApiSettings.IdentityBaseUri,
                  Policy = new DiscoveryPolicy { RequireHttps = false }
              });

            if (discovery.IsError)
            {
                throw discovery.Exception;
            }

            ClientCredentialsTokenRequest clientCredentialsTokenRequest = new()
            {
                ClientId = _clientSettings.WebClient.ClientId,
                ClientSecret = _clientSettings.WebClient.ClientSecret,
                Address = discovery.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            if (newToken.IsError)
            {
                throw newToken.Exception;
            }

            await _clientAccessTokenCache.SetAsync("WebClientToken", newToken.AccessToken,newToken.ExpiresIn);

            return newToken.AccessToken;
        }
    }
}
