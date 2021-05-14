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
            BaseAddress = new Uri("https://localhost:5001/api/")
        };
    }

    public async Task<Player> CreatePlayer(string name)
    {
        var newPlayer = new NewPlayer {Name = name};
        var json = JsonConvert.SerializeObject(newPlayer);
        var subUri = "players/new";
        return await Post<Player>(json, subUri);
    }

    public async Task<Player[]> GetAllPlayers()
    {
        var address = CombineUri(Client.BaseAddress.ToString(), "players/get-all");
        Debug.Log(address);
        var response = await Client.GetStringAsync(address);
        return JsonConvert.DeserializeObject<Player[]>(response);
    }

    private async Task<T> Post<T>(string json, string subUri)
    {
        var address = CombineUri(Client.BaseAddress.ToString(), subUri);
        var responseMessage = await Client.PostAsync(address, new StringContent(json, Encoding.UTF8, "application/json"));
        var responseObject = JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
        return responseObject;
    }
    
    public static string CombineUri(string uri1, string uri2)
    {
        uri1 = uri1.TrimEnd('/');
        uri2 = uri2.TrimStart('/');
        return $"{uri1}/{uri2}";
    }

    public class NewPlayer
    {
        public string Name { get; set; }
    }
}