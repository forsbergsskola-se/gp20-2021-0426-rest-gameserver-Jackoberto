using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitHubBrowser.Data;
using GitHubBrowser.Strategies;
using Newtonsoft.Json;

namespace GitHubBrowser
{
    public class GitHubAPI : IGitHubAPI
    {
        public void PrintUsers(string followersUrl)
        {
            var request = HttpRequest(followersUrl);
            var followers = JsonConvert.DeserializeObject<GitHubUser[]>(request);
            var userContainer = new UserContainer() {Users = followers};
            if (followers.Length < 1)
                Console.WriteLine("None");
            Console.WriteLine(userContainer.GetUserInfos());
        }

        public async Task<string> HttpRequest(IStrategy strategy, string parameter)
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
                    Console.WriteLine($"The {parameter} does not exist");
                return string.Empty;
            }
            return responseText;
        }
        
        private string HttpRequest(string url)
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
                responseText = httpClient.GetStringAsync(url).Result;
            }
            catch (HttpRequestException e)
            {
                return string.Empty;
            }
            return responseText;
        }
        
        public void PrintRepos(string reposUrl)
        {
            var request = HttpRequest(reposUrl);
            var repos = JsonConvert.DeserializeObject<Repo[]>(request);
            var repoContainer = new RepoContainer() {Repos = repos};
            Console.WriteLine(repoContainer.GetRepoInfos());
        }
    }
}