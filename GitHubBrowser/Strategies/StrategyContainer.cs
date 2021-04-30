using System.Collections.Generic;

namespace GitHubBrowser.Strategies
{
    public class StrategyContainer
    {
        public readonly Dictionary<string, IStrategy> strategies;

        public StrategyContainer(Dictionary<string, IStrategy> strategies)
        {
            this.strategies = strategies;
        }

        public static StrategyContainer DefaultStrategyContainer => new StrategyContainer(new Dictionary<string, IStrategy>
        {
            {"users", new UserStrategy()},
            {"orgs", new OrgsStrategy()},
            {"repos", new ReposStrategy()}
        });
    }
}