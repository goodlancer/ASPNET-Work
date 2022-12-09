using Domains.ConfigSetting;
using Domains.ConstModels;
using Domains.PostMania;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using RestSharp;

using System.Collections.Generic;
using System.Threading.Tasks;
using Domains.Dtos.Pcm;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ScolptioCRMCoreService.Services
{
    public class PostCardManiaService
    {
        private readonly PostCardManiaSetting _postCardManiaSetting;
        private readonly PostCardManiaUrl _postCardManiaUrl;

        public PostCardManiaService(IOptions<PostCardManiaSetting> postCardSetting, IOptions<PostCardManiaUrl> postCardManiaUrl)
        {
            _postCardManiaSetting = postCardSetting.Value;
            _postCardManiaUrl = postCardManiaUrl.Value;
        }

        public async Task<string> GetAccessToken()
        {
            var client = new RestClient(_postCardManiaUrl.LoginUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(_postCardManiaSetting);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
                return loginResponse.AccessToken;
            }
            return "";
        }

        public async Task<List<Domains.PostMania.Order>> GetAllOrdersAsync()
        {
            var token = await GetAccessToken();
            var orderResponse = await GetRequestToPostCardManiaApiAsync(_postCardManiaUrl.AllOrdersUrl, null, token);
            var orderList = JsonConvert.DeserializeObject<List<Domains.PostMania.Order>>(orderResponse);
            return orderList;
        }

        public async Task<Domains.PostMania.Order> GetOrderByIdAsync(int orderId)
        {
            var token = await GetAccessToken();
            var orderResponse = await GetRequestToPostCardManiaApiAsync(string.Format(_postCardManiaUrl.OrderDetailsUrl, orderId), null, token);
            var order = JsonConvert.DeserializeObject<Domains.PostMania.Order>(orderResponse);
            return order;
        }

        public string PostRequestToPostCardManiaApi(string url, object parameter, string token)
        {
            return "";
        }

        private async Task<string> GetRequestToPostCardManiaApiAsync(string url, object parameter, string token)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"bearer {token}");
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content;
            }
            return "";
        }
        private async Task<TResult> JsonPostRequestToPostCardManiaApiAsync<TResult>(string url, object payload, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonPayLoad = JsonConvert.SerializeObject(payload);
            var response = await client.PostAsync(url, new StringContent(jsonPayLoad, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(responseStr);
        }

        public async Task<OrderResponse> PlaceNewOrder(List<Domains.Dtos.Pcm.Order> orders)
        {
            var token = await GetAccessToken();
            var url = _postCardManiaUrl.PlaceOrderUrl;
            return await JsonPostRequestToPostCardManiaApiAsync<OrderResponse>(url, orders, token);
        }

    }
}
