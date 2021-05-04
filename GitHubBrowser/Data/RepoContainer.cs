using System;
using System.Collections;
using System.Collections.Generic;

namespace GitHubBrowser.Data
{
    public class RepoContainer : IEnumerable<Repo>
    {
        public Repo[] Repos { get; set; }

        public void PrintRepoInfo()
        {
            Console.WriteLine();
            foreach (var repo in this)
            {
                Console.WriteLine($"Name: {repo.Name}");
                Console.WriteLine($"Url: {repo.HtmlUrl}");
                Console.WriteLine();
            }
        }

        public IEnumerator<Repo> GetEnumerator() => ((IEnumerable<Repo>) Repos).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Repos.GetEnumerator();
    }
}