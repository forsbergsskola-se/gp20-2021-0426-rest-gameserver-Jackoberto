using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpResult = HttpRequest("milk.com");
            
            SaveToFile(httpResult);
            Console.WriteLine(httpResult);
            Console.WriteLine("Refs");
            foreach (var link in GetLinks(httpResult))
            {
                Console.WriteLine(link);
            }
        }

        static IEnumerable<string> GetLinks(string html)
        {
            var splits = html.Split("<li><a href=");
            for (var i = 1; i < splits.Length; i++)
            {
                splits[i] = splits[i].Insert(0, "<li><a href=");
                splits[i] = splits[i].Remove(splits[i].IndexOf("</li>", StringComparison.Ordinal) + 5);
            }

            return splits;
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
            result = result.Remove(result.IndexOf("</html>", StringComparison.Ordinal) + 7);
            return result;
        }

        static void SaveToFile(string html)
        {
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "File.xml"), html);
        }
    }
}
