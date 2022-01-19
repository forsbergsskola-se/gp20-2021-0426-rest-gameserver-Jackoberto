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

        public static readonly IGitHubAPI DefaultGitHubApi = new GitHubAPI();

        public static StrategyContainer DefaultStrategyContainer => new StrategyContainer(new Dictionary<string, IStrategy>
        {
            {"users", new UserStrategy(DefaultGitHubApi)},
            {"orgs", new OrgsStrategy(DefaultGitHubApi)},
            {"repos", new ReposStrategy(DefaultGitHubApi)}
        });
    }
}