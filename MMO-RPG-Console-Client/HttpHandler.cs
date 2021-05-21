using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MMO_RPG_Console_Client
{
    public class HttpHandler : IHttpHandler
    {
        private HttpClient Client { get; }
        public HttpHandler()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/api/players")
            };
        }

        public async Task<Player> CreatePlayer(NewPlayer player)
        {
            return await Post<Player>(player);
        }

        public async Task<Player[]> GetAllPlayers()
        {
            return await Get<Player[]>();
        }

        public async Task<Player> GetPlayer(string player)
        {
            return await Get<Player>("", player);
        }

        public async Task<Player> ModifyPlayer(ModifiedPlayer player, Guid guid)
        {
            return await Put<Player>(player, $"?guid={guid}");
        }

        private async Task<T> Get<T>(string subUri = "", string parameter = "")
        {
            var address = CombineUri(CombineUri(Client.BaseAddress!.ToString(), subUri), parameter);
            var response = await Client.GetStringAsync(address);
            return JsonConvert.DeserializeObject<T>(response);
        }
        
        private async Task<T> Put<T>(object content, string subUri = "")
        {
            var address = CombineUri(Client.BaseAddress!.ToString(), subUri);
            var responseMessage = await Client.PutAsync(address, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));
            var responseObject = JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
            return responseObject;
        }

        private async Task<T> Post<T>(object content, string subUri = "")
        {
            var address = CombineUri(Client.BaseAddress!.ToString(), subUri);
            var responseMessage = await Client.PostAsync(address, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));
            var responseObject = JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
            return responseObject;
        }
    
        public static string CombineUri(string uri1, string uri2)
        {
            if (string.IsNullOrEmpty(uri2))
                return uri1;
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return $"{uri1}/{uri2}";
        }
    }
}