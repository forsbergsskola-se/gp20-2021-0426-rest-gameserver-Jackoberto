using System.Threading.Tasks;
using GitHubBrowser.Strategies;

namespace GitHubBrowser
{
    public interface IGitHubAPI
    {
        public void PrintUsers(string followersUrl);
        public Task<string> HttpRequest(IStrategy strategy, string parameter);
        public void PrintRepos(string reposUrl);
    }
}