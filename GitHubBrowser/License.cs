using System.Text;

namespace GitHubBrowser
{
    public class License : ILicense
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string SpdxId { get; set; }
        public string Url { get; set; }
        public string NodeId { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLine();
            var properties = GetType().GetProperties();
            foreach (var t in properties)
            {
                var value = t.GetValue(this) ?? "Null";
                if (value is string s)
                    if (string.IsNullOrEmpty(s))
                        value = "Null";
                str.AppendLine($"{t.Name} = {value}");
            }

            var indent = "    ";
            var result = indent + str.ToString().Replace("\n", "\n" + indent);
            return result;
        }
    }
}