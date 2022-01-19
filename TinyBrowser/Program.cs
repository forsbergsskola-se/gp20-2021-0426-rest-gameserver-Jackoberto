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
        private static Stack<FullUrl> subURLS = new ();
        static void Main(string[] args)
        {
            var currentSite = "acme.com";
            var subPath = "";
            var currentUrl = new FullUrl(currentSite, subPath);

            while (true)
            {
                var httpResult = HttpRequest(currentUrl.domain, currentUrl.path);
            
                SaveToFile(httpResult);
                var title = GetTitle(httpResult);
                Console.WriteLine($"Title: {title}");
                var links = GetLinks(httpResult).ToArray();
                for (var i = 0; i < links.Length; i++)
                {
                    Console.WriteLine($"{i+1} {links[i].Title}: ({links[i].Link})");
                }

                var userChose = ListenForInput(links.Length);

                if (userChose == 0)
                {
                    currentUrl = subURLS.Count > 1 ? subURLS.Pop() : subURLS.Peek();
                    continue;
                }
                
                var chosenLink = links[userChose - 1];
                subURLS.Push(currentUrl);
                if (IsDomain(chosenLink.Link))
                {
                    currentUrl = SplitLink(chosenLink.Link);
                }
                else
                {
                    subPath = currentUrl.path.CombineUri(chosenLink.Link);
                    subPath = subPath.TrimStart('/');
                    currentUrl.path = subPath;
                }
            }
        }

        private static bool IsDomain(string link)
        {
            return link.StartsWith("http://");
        }
        
        private static FullUrl SplitLink(string link)
        {
            link = link.Remove(0, 7);
            var index = 0;
            for (var i = 0; i < link.Length; i++)
            {
                if (link[i] == '/')
                {
                    index = i;
                    break;
                }
            }
            
            return new FullUrl(link.Substring(0, index), link.Substring(index));
        }

        static int ListenForInput(int pageAmount)
        {
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("Type A Number To Open Link Or 0 To Go Back");
                var readLine = Console.ReadLine();
                if (int.TryParse(readLine, out var userChose))
                {
                    if (userChose <= pageAmount && userChose >= 0) 
                        return userChose;
                    Console.WriteLine($"{userChose} is not a valid link");
                    continue;
                }
                Console.WriteLine($"{readLine} is not a valid number");    
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

        static IEnumerable<LinkAndTitle> GetLinks(string html)
        {
            var splits = html.Split("<a href=\"");
            var linksAndTitles = new List<LinkAndTitle>();
            splits = splits.Skip(1).ToArray();
            foreach (var t in splits)
            {
                var link = t.TakeWhile(c => c != '"').ToArray();
                var partAfterLink = t[link.Length..];
                var startIndex = partAfterLink.IndexOf('>');
                var endIndex = partAfterLink.IndexOf("</a>", StringComparison.Ordinal);
                var title = partAfterLink.Substring(startIndex + 1, endIndex - startIndex - 1).Replace("<b>", "").Replace("</b>", "");
                if (title.StartsWith("<img"))
                    title = "Image";
                linksAndTitles.Add(new LinkAndTitle()
                {
                    Link = new string(link),
                    Title = title
                });
            }

            return linksAndTitles;
        }

        static string HttpRequest(string url, string subUrl)
        {
            var result = string.Empty;
            var fullUrl = string.IsNullOrEmpty(subUrl) ? url : url.CombineUri(subUrl);
            Console.WriteLine($"\nCurrent URL {fullUrl}");
            using var tcpClient = new TcpClient(url, 80); 
            var stream = tcpClient.GetStream();
            var builder = new StringBuilder();
            builder.AppendLine($"GET /{subUrl} HTTP/1.1");
            builder.AppendLine($"Host: {url}");
            builder.AppendLine("Connection: close");
            builder.AppendLine();
            var header = Encoding.ASCII.GetBytes(builder.ToString());
            stream.Write(header, 0, header.Length);
            var streamReader = new StreamReader(stream);
            // var data = new byte[tcpClient.ReceiveBufferSize];
            // stream.Read(data, 0, tcpClient.ReceiveBufferSize);
            // result = Encoding.ASCII.GetString(data);
            result = streamReader.ReadToEnd();
            result = result.ToLower();
            result = result.Remove(result.IndexOf("</html>", StringComparison.Ordinal) + 7);
            streamReader.Dispose();
            stream.Dispose();
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
}
