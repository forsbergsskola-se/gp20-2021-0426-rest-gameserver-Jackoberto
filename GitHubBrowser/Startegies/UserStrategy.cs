using System;
using GitHubBrowser.Data;

namespace GitHubBrowser.Startegies
{
    public class UserStrategy : IStrategy
    {
        public string BaseUrl => "https://api.github.com/users";
        public string AskForSearchParameters()
        {
            Console.WriteLine("Write A GitHub User To Search");
            var user = Console.ReadLine();
            return user;
        }

        public void DealWithProvidedJson(string json)
        {
            GitHubApplication.PrintJsonInfo<GitHubUser>(json);
        }
    }
}