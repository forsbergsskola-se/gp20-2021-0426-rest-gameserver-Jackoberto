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
            Console.WriteLine("User Info");
            Console.WriteLine(response.ToString());
        }
    }
}