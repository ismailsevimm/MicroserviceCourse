﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MSCourse.Web.Exceptions;
using MSCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MSCourse.Web.Handlers
{
    public class ResourceOwnerPasswordTokenHandler:DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService _identityService;
        private readonly ILogger<ResourceOwnerPasswordTokenHandler> _logger;

        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityService identityService, ILogger<ResourceOwnerPasswordTokenHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityService = identityService;
            _logger = logger;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var newAccessTokenResponse = await _identityService.GetAccessTokenByRefreshToken();

                if (newAccessTokenResponse != null)
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newAccessTokenResponse.AccessToken);

                    response = await base.SendAsync(request, cancellationToken);
                }
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // Hata fırlatılacak
                // Middleware üzerinden yönlendirme yapılacak.

                throw new CustomUnAuthorizeException();
            }

            return response;
        }
    }
}
