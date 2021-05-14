using System;
using System.Net.Http;

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
}