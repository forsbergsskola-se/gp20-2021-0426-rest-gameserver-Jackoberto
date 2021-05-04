using System;
using System.Text.RegularExpressions;
using GitHubBrowser.Data;
using Newtonsoft.Json;

namespace GitHubBrowser.Strategies
{
    public class UserStrategy : IStrategy
    {
        public string BaseUrl => "https://api.github.com/users";
        private string currentUser;
        public string AskForSearchParameters()
        {
            Console.WriteLine("Write A GitHub User To Search");
            var user = Console.ReadLine();
            currentUser = user;
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
                PrintRepos(response.ReposUrl);
            }
            
            if (whatToDisplay.Equals("followers", StringComparison.OrdinalIgnoreCase))
            {
                PrintFollowers(response.FollowersUrl);
            }
            
            if (whatToDisplay.Equals("following", StringComparison.OrdinalIgnoreCase))
            {
                PrintFollowers(parameterRegex.Replace(response.FollowingUrl, ""));
            }
        }

        private void PrintFollowers(string followersUrl)
        {
            var request = GitHubApplication.HttpRequest(followersUrl);
            var followers = JsonConvert.DeserializeObject<GitHubUser[]>(request);
            var userContainer = new UserContainer() {Users = followers};
            Console.WriteLine(userContainer.GetUserInfos());
        }

        private void PrintRepos(string reposUrl)
        {
            var request = GitHubApplication.HttpRequest(reposUrl);
            var repos = JsonConvert.DeserializeObject<Repo[]>(request);
            var repoContainer = new RepoContainer() {Repos = repos};
            Console.WriteLine(repoContainer.GetRepoInfos());
        }
    }
}