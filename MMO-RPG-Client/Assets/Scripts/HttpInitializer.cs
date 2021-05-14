using System.Linq;
using UnityEngine;

public class HttpInitializer : MonoBehaviour
{
    private IHttpHandler HttpHandler { get; set; }
    private void Awake()
    {
        HttpHandler = new HttpHandler();
        var objects = FindObjectsOfType<MonoBehaviour>().OfType<INeedHttpHandler>();
        foreach (var obj in objects)
        {
            obj.HttpHandler = HttpHandler;
        }
    }
}