using UnityEngine;

public class CreatePlayer : MonoBehaviour, INeedHttpHandler
{
    public IHttpHandler HttpHandler { get; set; }
}