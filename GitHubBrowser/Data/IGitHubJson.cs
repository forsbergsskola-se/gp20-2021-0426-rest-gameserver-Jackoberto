namespace GitHubBrowser.Data
{
    public interface IGitHubJson
    {
        public string Type { get; set; }
    }

    public class GitHubJson : IGitHubJson
    {
        public string Type { get; set; }
    }
}