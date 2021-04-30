using System.Threading.Tasks;
using GitHubBrowser.Startegies;

namespace GitHubBrowser
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var app = new GitHubApplication(Strategies.DefaultStrategies);
            await app.Start();
        }
    }
}