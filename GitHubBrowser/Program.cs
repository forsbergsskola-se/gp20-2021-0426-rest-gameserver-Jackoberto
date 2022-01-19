using System.Threading.Tasks;
using GitHubBrowser.Strategies;

namespace GitHubBrowser
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var app = new GitHubApplication(StrategyContainer.DefaultStrategyContainer, new GitHubAPI());
            await app.Start();
        }
    }
}