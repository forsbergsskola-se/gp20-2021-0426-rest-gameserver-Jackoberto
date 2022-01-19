using System;
using System.Collections.Generic;

namespace GitHubBrowser
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Tuple<string, string>> GetPropertiesFormatted(this object obj) 
        {
            var properties = obj.GetType().GetProperties();
            foreach (var t in properties)
            {
                var value = t.GetValue(obj) ?? "Null";
                if (value is string s)
                    if (string.IsNullOrEmpty(s))
                        value = "Null";
                yield return new(t.Name, value.ToString());
            }
        }
    }
}