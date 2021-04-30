using System;
using GitHubBrowser.Data;

namespace GitHubBrowser
{
    public class OrgsStrategy : IStrategy
    {
        public string BaseUrl => "https://api.github.com/orgs";
        public string AskForSearchParameters()
        {
            Console.WriteLine("Write A GitHub Org To Search");
            var user = Console.ReadLine();
            return user;
        }

        public void DealWithProvidedJson(string json)
        {
            GitHubApplication.PrintJsonInfo<Organization>(json);
        }
    }
}