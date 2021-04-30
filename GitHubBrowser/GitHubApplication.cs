using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GitHubBrowser
{
    public class GitHubApplication
    {
        private readonly Strategies strategies;
        private Dictionary<string, IStrategy> ApiStrategies => strategies.strategies;

        public GitHubApplication(Strategies strategies)
        {
            this.strategies = strategies;
        }

        public async Task Start()
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            };
            while (true)
            {
                var response = await GetResponse();
                if (response == null)
                    continue;
                var (responseText, responseType) = response;
                if (ApiStrategies.TryGetValue(responseType, out var usage))
                {
                    usage.DealWithProvidedJson(responseText);
                }
            }
        }

        private async Task<Tuple<string, string>> GetResponse()
        {
            Console.WriteLine("What Do You Wanna Search For?");
            Console.Write("Possible Searches Are");
            var startMethods = ApiStrategies;
            foreach (var (key,value) in startMethods)
            {
                Console.Write($", {key}");
            }
            Console.WriteLine();
            var input = Console.ReadLine();
            var inputAsLower = input.ToLower();
            if (startMethods.TryGetValue(inputAsLower, out var usage))
            {
                var user = usage.AskForSearchParameters();
                var response = await HttpRequest(usage, user);
                return new Tuple<string, string>(response, inputAsLower);
            }
            
            return null;
        }

        private static async Task<string> HttpRequest(IStrategy strategy, string parameter)
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
                responseText = await httpClient.GetStringAsync($"{strategy.BaseUrl}/{parameter}");
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                    Console.WriteLine($"The user {parameter} does not exist");
                return string.Empty;
            }
            return responseText;
        }
        
        public static void PrintJsonInfo<T>(string json)
        {
            var response = JsonConvert.DeserializeObject<T>(json);
            PrintPropertyValues(response);
        }

        private static void PrintPropertyValues(object gitHubJson)
        {
            Console.WriteLine();
            foreach (var (name, value) in gitHubJson.GetPropertiesFormatted())
            {
                Console.WriteLine($"{name} = {value}");
            }
            Console.WriteLine();
        }
    }
}