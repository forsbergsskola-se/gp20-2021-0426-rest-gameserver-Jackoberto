using System;
using System.Net.Sockets;

namespace TinyBrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        string HttpRequest()
        {
            var result = string.Empty;
            using (var tcpClient = new TcpClient("www."))
        }
    }
}
