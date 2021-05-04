using System;
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
            Console.WriteLine("Possible Searches Are repos, followers");
            
            var whatToDisplay = Console.ReadLine();
            if (whatToDisplay.Equals("repos", StringComparison.OrdinalIgnoreCase))
            {
                PrintRepos(response.ReposUrl);
            }
            
            if (whatToDisplay.Equals("followers", StringComparison.OrdinalIgnoreCase))
            {
                PrintFollowers(response.FollowersUrl);
            }
        }

        private void PrintFollowers(string followersUrl)
        {
            var request = GitHubApplication.HttpRequest(followersUrl);
            var followers = JsonConvert.DeserializeObject<GitHubUser[]>(request);
            Console.WriteLine();
            foreach (var user in followers)
            {
                Console.WriteLine($"Name: {user.Login}");
                Console.WriteLine($"Url: {user.HtmlUrl}");
                Console.WriteLine();
            }
        }

        private void PrintRepos(string reposUrl)
        {
            var request = GitHubApplication.HttpRequest(reposUrl);
            var repos = JsonConvert.DeserializeObject<Repo[]>(request);
            Console.WriteLine();
            foreach (var repo in repos)
            {
                Console.WriteLine($"Name: {repo.Name}");
                Console.WriteLine($"Url: {repo.HtmlUrl}");
                Console.WriteLine();
            }
        }
    }
}