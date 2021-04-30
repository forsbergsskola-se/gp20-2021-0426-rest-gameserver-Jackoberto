﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GitHubBrowser
{
    class Program
    {
        private static Dictionary<string, Action<string>> _endMethods = new()
        {
            {"user", AskAboutUser},
            {"orgs", AskAboutOrg},
            {"repos", AskAboutRepo}
        };

        private static Dictionary<string, Func<string>> _startMethods = new()
        {
            {"users", AskForUser},
            {"orgs", AskForOrg},
            {"repos", AskForRepo}
        };

        private static JsonSerializerSettings _deserializerSettings;

        static async Task Main(string[] args)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            _deserializerSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            };
            while (true)
            {
                var (responseText, type) = await GetResponse();
                if (responseText == string.Empty)
                    continue;

                if (_endMethods.TryGetValue(type, out var action))
                {
                    action.Invoke(responseText);
                }
            }
        }

        private static async Task<Tuple<string, string>> GetResponse()
        {
            Console.WriteLine("What Do You Wanna Search For?");
            Console.Write("Possible Searches Are");
            var startMethods = _startMethods;
            foreach (var (key,value) in startMethods)
            {
                Console.Write($" {key}");
            }
            Console.WriteLine();
            var input = Console.ReadLine();
            if (startMethods.TryGetValue(input, out var func))
            {
                var user = func.Invoke();
                var response = await HttpRequest(input, user);
                return new Tuple<string, string>(response, input);
            }
            
            return null;
        }

        private static async Task<string> HttpRequest(string path, string user)
        {
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
                responseText = await httpClient.GetStringAsync($"https://api.github.com/{path}/{user}");
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                    Console.WriteLine($"The user {user} does not exist");
                return string.Empty;
            }
            return responseText;
        }
        
        private static void AskAboutUser(string json)
        {
            var response = JsonConvert.DeserializeObject<GitHubUser>(json, _deserializerSettings);
            GetPropertyValue(response);
        }

        private static void AskAboutOrg(string json)
        {
            var response = JsonConvert.DeserializeObject<Organization>(json, _deserializerSettings);
            GetPropertyValue(response);
        }
        
        private static void AskAboutRepo(string json)
        {
            var response = JsonConvert.DeserializeObject<Repo>(json, _deserializerSettings);
            GetPropertyValue(response);
        }

        private static void GetPropertyValue(object gitHubJson)
        {
            PrintAllProperties(gitHubJson);
            /*while (true)
            {
                // Console.WriteLine("What do you wanna know?");
                // var propertyInfo = AskForProperty(gitHubJson);
                // if (propertyInfo != null)
                // {
                //     var value = propertyInfo.GetValue(gitHubJson);
                //     Console.WriteLine(value == null
                //         ? $"Value Of {propertyInfo.Name} Is Null"
                //         : $"Value Of {propertyInfo.Name} Is {value}");
                //     break;
                // }
                // Console.WriteLine("Invalid Property");
            }*/
        }

        private static void PrintAllProperties(object gitHubJson)
        {
            var properties = gitHubJson.GetType().GetProperties();
            foreach (var t in properties)
            {
                var value = t.GetValue(gitHubJson) ?? "Null";
                if (value is string s)
                    if (string.IsNullOrEmpty(s))
                        value = "Null";
                Console.WriteLine($"{t.Name} = {value}");
            }
            Console.WriteLine();
        }

        static PropertyInfo AskForProperty(IGitHubJson gitHubJson)
        {
            var userWantsToKnow = Console.ReadLine();
            return (gitHubJson.GetType().GetProperties())
                                        .FirstOrDefault(property => property.Name
                                        .Equals(userWantsToKnow
                                        .Replace(" ", string.Empty), StringComparison.OrdinalIgnoreCase));
        }
        
        private static string AskForRepo()
        {
            Console.WriteLine("Write A GitHub User And Repo Formatted Like {user}/{repo}");
            var user = Console.ReadLine();
            return user;
        }

        static string AskForUser()
        {
            Console.WriteLine("Write A GitHub User To Search");
            var user = Console.ReadLine();
            return user;
        }
        
        private static string AskForOrg()
        {
            Console.WriteLine("Write A GitHub Org To Search");
            var user = Console.ReadLine();
            return user;
        }
    }
}
