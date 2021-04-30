using System.Threading.Tasks;


namespace GitHubBrowser
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var app = new GitHubApplication(Strategies.DefaultStrategies);
            await app.Start();
        }
    }
}
