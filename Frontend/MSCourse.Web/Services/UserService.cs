﻿using MSCourse.Web.Models;
using MSCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MSCourse.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserViewModel> GetUser()
        {
            return await _httpClient.GetFromJsonAsync<UserViewModel>("/api/users/getuser");
        }
    }
}
