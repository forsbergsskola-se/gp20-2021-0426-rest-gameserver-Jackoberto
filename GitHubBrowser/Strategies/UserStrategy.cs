using System;
using System.Text.RegularExpressions;
using GitHubBrowser.Data;
using Newtonsoft.Json;

namespace GitHubBrowser.Strategies
{
    public class UserStrategy : IStrategy
    {
        public string BaseUrl => "https://api.github.com/users";
        private IGitHubAPI gitHubApi;

        public UserStrategy(IGitHubAPI gitHubApi)
        {
            this.gitHubApi = gitHubApi;
        }

        public string AskForSearchParameters()
        {
            Console.WriteLine("Write A GitHub User To Search");
            var user = Console.ReadLine();
            return user;
        }

        public void DealWithProvidedJson(string json)
        {
            var response = JsonConvert.DeserializeObject<GitHubUser>(json);
            Console.WriteLine("\n" +
                              "User Info");
            Console.WriteLine(response.ToString());
            Console.WriteLine("What Do You Want To Get?");
            Console.WriteLine("Possible Searches Are repos, followers, following");
            var parameterRegex = new Regex(@"\{([^\}]+)\}");
            var whatToDisplay = Console.ReadLine();
            if (whatToDisplay.Equals("repos", StringComparison.OrdinalIgnoreCase))
            {
                gitHubApi.PrintRepos(response.ReposUrl);
            }
            
            if (whatToDisplay.Equals("followers", StringComparison.OrdinalIgnoreCase))
            {
                gitHubApi.PrintUsers(response.FollowersUrl);
            }
            
            if (whatToDisplay.Equals("following", StringComparison.OrdinalIgnoreCase))
            {
                gitHubApi.PrintUsers(parameterRegex.Replace(response.FollowingUrl, ""));
            }
        }
    }
}