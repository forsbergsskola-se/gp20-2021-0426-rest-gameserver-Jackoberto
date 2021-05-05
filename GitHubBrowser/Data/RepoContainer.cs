using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GitHubBrowser.Data
{
    public class RepoContainer : IEnumerable<IRepo>
    {
        public IRepo[] Repos { get; init; }

        public string GetRepoInfos()
        {
            var str = new StringBuilder();
            str.AppendLine();
            foreach (var repo in this)
            {
                str.AppendLine($"Name: {repo.Name}");
                str.AppendLine($"Url: {repo.HtmlUrl}");
                str.AppendLine($"Description: {repo.Description}");
                str.AppendLine();
            }

            return str.ToString();
        }

        public IEnumerator<IRepo> GetEnumerator() => ((IEnumerable<IRepo>) Repos).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Repos.GetEnumerator();
    }
}