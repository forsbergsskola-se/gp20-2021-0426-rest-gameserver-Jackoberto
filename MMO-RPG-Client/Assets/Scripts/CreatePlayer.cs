using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreatePlayer : MonoBehaviour, INeedHttpHandler
{
    public IHttpHandler HttpHandler { get; set; }

    public UnityEvent<string> playerGUID;

    public InputField inputField;

    public async void Create()
    {
        var player = await HttpHandler.CreatePlayer(inputField.text);
        playerGUID.Invoke(player.Id.ToString());
    }
}