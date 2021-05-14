using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GetPlayer : MonoBehaviour, INeedHttpHandler
{
    public IHttpHandler HttpHandler { get; set; }

    public UnityEvent<string> playerToString;

    public InputField inputField;

    public async void Get()
    {
        var player = await HttpHandler.GetPlayer(inputField.text);
        playerToString.Invoke(player.ToString());
    }
    
    public async void GetAll()
    {
        var players = await HttpHandler.GetAllPlayers();
        var s = players.Aggregate(string.Empty, (current, player) => current + player.Name + " ");
        playerToString.Invoke(s);
    }
}
