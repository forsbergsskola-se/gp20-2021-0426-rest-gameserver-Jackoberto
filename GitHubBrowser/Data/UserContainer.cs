using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GitHubBrowser.Data
{
    public class UserContainer : IEnumerable<IGitHubUser>
    {
        public IGitHubUser[] Users { get; init; }

        public string GetUserInfos()
        {
            var str = new StringBuilder();
            str.AppendLine();
            foreach (var repo in this)
            {
                str.AppendLine($"Name: {repo.Login}");
                str.AppendLine($"Url: {repo.HtmlUrl}");
                str.AppendLine();
            }

            return str.ToString();
        }

        public IEnumerator<IGitHubUser> GetEnumerator() => ((IEnumerable<IGitHubUser>) Users).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Users.GetEnumerator();
    }
}