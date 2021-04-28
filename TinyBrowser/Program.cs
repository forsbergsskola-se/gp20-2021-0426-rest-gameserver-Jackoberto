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
        public static string CurrentSite;
        static void Main(string[] args)
        {
            CurrentSite = "acme.com";
            var subPath = "";
            while (true)
            {
                var httpResult = HttpRequest(CurrentSite, subPath);
            
                SaveToFile(httpResult);
                var links = GetLinks(httpResult).ToArray();
                for (var i = 0; i < links.Length; i++)
                {
                    Console.WriteLine($"{i+1}: {links[i]}");
                }

                var readLine = Console.ReadLine();
                var userChose = int.Parse(readLine);
                subPath = links[userChose - 1];
            }
        }

        static IEnumerable<string> GetLinks(string html)
        {
            var splits = html.Split("<a href=\"");
            var links = new List<string>();
            splits = splits.Skip(1).ToArray();
            for (var i = 0; i < splits.Length; i++)
            {
                var link = splits[i].TakeWhile(c => c != '"');
                links.Add(new string(link.ToArray()));
            }

            return links;
        }
        
        static IEnumerable<string> GetTitles(string html)
        {
            var splits = html.Split("<a href=\"");
            var titles = new List<string>();
            splits = splits.Skip(1).ToArray();
            for (var i = 0; i < splits.Length; i++)
            {
                var title = splits[i].SkipWhile(c => c != '"');
                titles.Add(new string(title.ToArray()));
            }

            return titles;
        }

        static string HttpRequest(string url, string subPath)
        {
            var result = string.Empty;
            using var tcpClient = new TcpClient(url, 80);
            using var stream = tcpClient.GetStream();
            var builder = new StringBuilder();
            builder.AppendLine("GET / HTTP/1.1");
            builder.AppendLine(!string.IsNullOrEmpty(subPath) ? $"Host: {url}/{subPath}" : $"Host: {url}");
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
