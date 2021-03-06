namespace TinyBrowser
{
    public static class StringExtensions
    {
        public static string CombineUri(this string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return $"{uri1}/{uri2}";
        }
    }
}