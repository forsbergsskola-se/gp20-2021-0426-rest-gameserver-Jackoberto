namespace GitHubBrowser
{
    public interface IStrategy
    {
        public string BaseUrl { get; }
        public string AskForSearchParameters();
        public void DealWithProvidedJson(string json);
    }
}