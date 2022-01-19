using System;
using System.Text.RegularExpressions;
using GitHubBrowser.Data;
using Newtonsoft.Json;

namespace GitHubBrowser.Strategies
{
    public class OrgsStrategy : IStrategy
    {
        private readonly IGitHubAPI gitHubApi;

        public OrgsStrategy(IGitHubAPI gitHubApi)
        {
            this.gitHubApi = gitHubApi;
        }

        public string BaseUrl => "https://api.github.com/orgs";
        public string AskForSearchParameters()
        {
            Console.WriteLine("Write A GitHub Org To Search");
            var user = Console.ReadLine();
            return user;
        }

        public void DealWithProvidedJson(string json)
        {
            var response = JsonConvert.DeserializeObject<Organization>(json);
            Console.WriteLine("\n" +
                              "Org Info");
            Console.WriteLine(response.ToString());
            Console.WriteLine("What Do You Want To Get?");
            Console.WriteLine("Possible Searches Are repos, members");
            var parameterRegex = new Regex(@"\{([^\}]+)\}");
            var whatToDisplay = Console.ReadLine();
            if (whatToDisplay.Equals("repos", StringComparison.OrdinalIgnoreCase))
            {
                gitHubApi.PrintRepos(response.ReposUrl);
            }
            
            if (whatToDisplay.Equals("members", StringComparison.OrdinalIgnoreCase))
            {
                gitHubApi.PrintUsers(parameterRegex.Replace(response.PublicMembersUrl, ""));
            }
        }
    }
}