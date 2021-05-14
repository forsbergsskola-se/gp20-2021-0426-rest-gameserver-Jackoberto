using System.Threading.Tasks;
using UnityEngine;

public class MainMenu : MonoBehaviour, INeedHttpHandler
{
    Task<Player[]> players;

    void OnGUI(){
        if (players?.IsCompleted == false)
        {
            GUILayout.Label("Waiting For Response");
            return;
        }

        if (GUILayout.Button("Search Players"))
        {
            players = HttpHandler.GetAllPlayers();
            return;
        }

        if (GUILayout.Button("Create Player"))
        {
            // TODO Add Some Method Call
            return;
        }

        foreach (var player in players.Result)
        {
            if (GUILayout.Button(player.Name))
            {
                // TODO Add Some Method Call
            }
        }
    }

    public IHttpHandler HttpHandler { get; set; }
}
