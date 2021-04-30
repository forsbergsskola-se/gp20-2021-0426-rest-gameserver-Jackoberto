using System.Collections.Generic;

namespace GitHubBrowser
{
    public class Strategies
    {
        public readonly Dictionary<string, IStrategy> strategies;

        public Strategies(Dictionary<string, IStrategy> strategies)
        {
            this.strategies = strategies;
        }

        public static Strategies DefaultStrategies => new Strategies(new Dictionary<string, IStrategy>
        {
            {"users", new UserStrategy()},
            {"orgs", new OrgsStrategy()},
            {"repos", new ReposStrategy()}
        });
    }
}