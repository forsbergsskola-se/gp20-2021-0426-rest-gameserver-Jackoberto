namespace GitHubBrowser.Data
{
    public interface ILicense
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string SpdxId { get; set; }
        public string Url { get; set; }
        public string NodeId { get; set; }
    }
}