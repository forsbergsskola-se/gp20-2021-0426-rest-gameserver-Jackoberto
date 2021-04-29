using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitHubBrowser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                var user = AskForUser();
                var httpClient = new HttpClient
                {
                    DefaultRequestHeaders =
                    {
                        Accept = {new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")},
                        UserAgent = {ProductInfoHeaderValue.Parse("request")}
                    }
                };
                var responseText = "";
                try
                {
                    responseText = await httpClient.GetStringAsync($"https://api.github.com/users/{user}");
                }
                catch (HttpRequestException e)
                {
                    if (e.StatusCode == HttpStatusCode.NotFound)
                        Console.WriteLine($"The user {user} does not exist");
                    continue;
                }
                
                IGitHubUser response = JsonConvert.DeserializeObject<GitHubUser>(responseText);
                // IGitHubUser response = new GitHubUser();
                // JsonConvert.PopulateObject(responseText, response);
                Console.WriteLine("What do you wanna know?");
                while (true)
                {
                    var propertyInfo = AskForProperty();
                    if (propertyInfo != null)
                    {
                        var value = propertyInfo.GetValue(response);
                        Console.WriteLine(value == null
                            ? $"Value Of {propertyInfo.Name} Is Null"
                            : $"Value Of {propertyInfo.Name} Is {value}");
                        break;
                    }
                    Console.WriteLine("Invalid Property");
                }
            }
        }

        static PropertyInfo AskForProperty()
        {
            var userWantsToKnow = Console.ReadLine();
            return (typeof(IGitHubUser).GetProperties()).FirstOrDefault(property => property.Name == userWantsToKnow);
        }

        static string AskForUser()
        {
            Console.WriteLine("Write A GitHub User To Search");
            var user = Console.ReadLine();
            return user;
        }
    }
}
