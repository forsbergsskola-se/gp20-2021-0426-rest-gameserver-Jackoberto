using System;
using GitHubBrowser.Data;

namespace GitHubBrowser.Strategies
{
    public class ReposStrategy : IStrategy
    {
        private readonly IGitHubAPI gitHubApi;

        public ReposStrategy(IGitHubAPI gitHubApi)
        {
            this.gitHubApi = gitHubApi;
        }

        public string BaseUrl => "https://api.github.com/repos";
        public string AskForSearchParameters()
        {
            Console.WriteLine("Write A GitHub User And Repo Formatted Like {user}/{repo}");
            var user = Console.ReadLine();
            return user;
        }

        public void DealWithProvidedJson(string json)
        {
            GitHubApplication.PrintJsonInfo<Repo>(json);
        }
    }
}