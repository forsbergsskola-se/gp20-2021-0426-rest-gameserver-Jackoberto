using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

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

    public async Task<Player> CreatePlayer(string name)
    {
        var newPlayer = new NewPlayer {Name = name};
        var json = JsonConvert.SerializeObject(newPlayer);
        return await Post<Player>(json);
    }

    public async Task<Player[]> GetAllPlayers()
    {
        return await Get<Player[]>();
    }

    public async Task<Player> GetPlayer(string player)
    {
        return await Get<Player>("", player);
    }

    private async Task<T> Get<T>(string subUri = "", string parameter = "")
    {
        var address = CombineUri(CombineUri(Client.BaseAddress.ToString(), subUri), parameter);
        Debug.Log(address);
        var response = await Client.GetStringAsync(address);
        return JsonConvert.DeserializeObject<T>(response);
    }

    private async Task<T> Post<T>(string json, string subUri = "")
    {
        var address = CombineUri(Client.BaseAddress.ToString(), subUri);
        var responseMessage = await Client.PostAsync(address, new StringContent(json, Encoding.UTF8, "application/json"));
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

    public class NewPlayer
    {
        public string Name { get; set; }
    }
}