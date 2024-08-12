using Microsoft.Extensions.Configuration;
using RandomUsers.Web.Models;
using System.Text.Json;

namespace RandomUsers.Web.Services
{
    public class RandomUsersApiService : IRandomUserApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public RandomUsersApiService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _configuration = config;
            _clientFactory = clientFactory;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            var address = _configuration.GetSection("API:address");

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(address.Value);

            var result = new List<UserModel>();

            var response = await client.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(stringResponse);

                foreach (UserModel user in apiResponse.Results)
                {
                    result.Add(user);
                }
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}
