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
        private static Stack<string> subURLS = new ();
        static void Main(string[] args)
        {
            CurrentSite = "acme.com";
            var subPath = "";
            while (true)
            {
                var httpResult = HttpRequest(CurrentSite, subPath);
            
                SaveToFile(httpResult);
                var title = GetTitle(httpResult);
                Console.WriteLine($"Title: {title}");
                var links = GetLinks(httpResult).ToArray();
                for (var i = 0; i < links.Length; i++)
                {
                    Console.WriteLine($"{i+1}: {links[i]}");
                }

                var readLine = Console.ReadLine();
                var userChose = int.Parse(readLine);
                if (userChose == 0)
                {
                    subPath = subURLS.Count > 1 ? subURLS.Pop() : subURLS.Peek();
                    continue;
                }
                var chosenLink = links[userChose - 1];
                subURLS.Push(subPath);
                subPath = subPath.CombineUri(chosenLink);
                subPath = subPath.TrimStart('/');
            }
        }

        private static string GetTitle(string html)
        {
            if (!html.Contains("<title>"))
                return "NO TITLE";
            var startIndex = html.IndexOf("<title>", StringComparison.Ordinal) + 7;
            var endIndex = html.IndexOf("</title>", StringComparison.Ordinal);
            var title = html.Substring(startIndex, endIndex - startIndex);
            return title;
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
        
        static IEnumerable<string> GetDescriptions(string html)
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

        static string HttpRequest(string url, string subUrl)
        {
            var result = string.Empty;
            var fullUrl = string.IsNullOrEmpty(subUrl) ? url : url.CombineUri(subUrl);
            Console.WriteLine(fullUrl);
            using var tcpClient = new TcpClient(url, 80);
            using var stream = tcpClient.GetStream();
            var builder = new StringBuilder();
            builder.AppendLine($"GET /{subUrl} HTTP/1.1");
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
        
        public static string CombineUri(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return $"{uri1}/{uri2}";
        }
    }

    public static class StringExtensions
    {
        public static string CombineUri(this string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return $"{uri1}/{uri2}";
        }
    }
}
