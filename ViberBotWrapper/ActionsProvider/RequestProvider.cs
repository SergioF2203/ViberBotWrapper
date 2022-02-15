using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Models;
using static System.Net.Mime.MediaTypeNames;

namespace ViberBotWebApp.ActionsProvider
{
    public class RequestProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public JsonSerializerOptions Options { get; set; } = new() { IgnoreNullValues = true };

        public RequestProvider(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task SendWebHookSetRequest(WebHookModel webhookbody)
        {
            var messageJSON = new StringContent(JsonSerializer.Serialize(webhookbody, Options), System.Text.Encoding.UTF8, Application.Json);


            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.CacheControl = new() { NoCache = true };

            var token = _configuration.GetSection("AuthenticationToken").Value;

            httpClient.DefaultRequestHeaders.Add("X-Viber-Auth-Token", token);

            var result = await httpClient.PostAsync(_configuration.GetSection("ViberURLs").GetSection("SetWebHookURL").Value, messageJSON);

        }

        public async Task<AccountInfo> GetAccountInfo()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.CacheControl = new() { NoCache = true };

            var token = _configuration.GetSection("AuthenticationToken").Value;

            httpClient.DefaultRequestHeaders.Add("X-Viber-Auth-Token", token);

            var res = await httpClient.PostAsync(_configuration.GetSection("ViberURLs").GetSection("AccountInfo").Value, null);
            var content = res.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<AccountInfo>(content.Result);
        }

        public async Task SendMessageRequest(SendedMessage message)
        {

            var messageJSON = new StringContent(JsonSerializer.Serialize(message, Options), System.Text.Encoding.UTF8, Application.Json);

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.CacheControl = new() { NoCache = true };

            httpClient.DefaultRequestHeaders.Add("X-Viber-Auth-Token", "4e4550b8e927d4dd-e23bf5bfa55ca026-277101619abddd6e");

            var result = await httpClient.PostAsync(_configuration.GetSection("ViberURLs").GetSection("SendMessageURL").Value, messageJSON);
        }

    }
}
