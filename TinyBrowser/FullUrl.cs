namespace TinyBrowser
{
    public struct FullUrl
    {
        public string domain;
        public string path;

        public FullUrl(string domain, string path)
        {
            this.domain = domain;
            this.path = path;
        }
    }
}