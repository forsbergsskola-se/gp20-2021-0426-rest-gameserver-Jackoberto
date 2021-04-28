using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpResult = HttpRequest("acme.com");
            
            SaveToFile(httpResult);
            foreach (var link in GetLinks(httpResult)) 
                Console.WriteLine(link);
        }

        static IEnumerable<string> GetLinks(string html)
        {
            var splits = html.Split("<a href=\"");
            var completedString = new List<string>();
            splits = splits.Skip(1).ToArray();
            for (var i = 0; i < splits.Length; i++)
            {
                splits[i] = splits[i].Remove(splits[i].IndexOf("</a>", StringComparison.Ordinal));
                var chars = splits[i].TakeWhile(c => c != '"');
                completedString.Add(new string(chars.ToArray()));
            }

            return completedString;
        }

        static string HttpRequest(string url)
        {
            var result = string.Empty;
            using var tcpClient = new TcpClient(url, 80);
            using var stream = tcpClient.GetStream();
            var builder = new StringBuilder();
            builder.AppendLine("GET / HTTP/1.1");
            builder.AppendLine($"Host: {url}");
            builder.AppendLine("Connection: close");
            builder.AppendLine();
            var header = Encoding.ASCII.GetBytes(builder.ToString());
            stream.Write(header, 0, header.Length);
            var data = new byte[tcpClient.ReceiveBufferSize];
            stream.Read(data, 0, tcpClient.ReceiveBufferSize);
            result = Encoding.ASCII.GetString(data);
            result = result.ToLower();
            result = result.Remove(result.IndexOf("</html>", StringComparison.Ordinal) + 7);
            return result;
        }

        static void SaveToFile(string html)
        {
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "File.txt"), html);
        }
    }
}
