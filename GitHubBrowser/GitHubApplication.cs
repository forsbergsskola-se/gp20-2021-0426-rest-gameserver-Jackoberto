using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubBrowser.Strategies;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GitHubBrowser
{
    public class GitHubApplication
    {
        private IGitHubAPI gitHubApi;
        private readonly StrategyContainer strategyContainer;
        private Dictionary<string, IStrategy> ApiStrategies => strategyContainer.strategies;

        public GitHubApplication(StrategyContainer strategyContainer, IGitHubAPI gitHubApi)
        {
            this.strategyContainer = strategyContainer;
            this.gitHubApi = gitHubApi;
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
                var response = await gitHubApi.HttpRequest(usage, user);
                return new Tuple<string, string>(response, inputAsLower);
            }
            
            return null;
        }

        public static void PrintJsonInfo<T>(string json)
        {
            var response = JsonConvert.DeserializeObject<T>(json);
            if (response.GetType().IsArray)
            {
                var array = response as Array;
                foreach (var arr in array)
                {
                    PrintPropertyValues(arr);
                }
            }
            else
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