using System;

namespace GitHubBrowser.Data
{
    public class GitHubUser : IGitHubUser
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public string NodeId { get; set; }
        public string AvatarUrl { get; set; }
        public string GravatarId { get; set; }
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string FollowersUrl { get; set; }
        public string FollowingUrl { get; set; }
        public string GistsUrl { get; set; }
        public string StarredUrl { get; set; }
        public string SubscriptionsUrl { get; set; }
        public string OrganizationsUrl { get; set; }
        public string ReposUrl { get; set; }
        public string EventsUrl { get; set; }
        public string ReceivedEventsUrl { get; set; }
        public string Type { get; set; }
        public bool SiteAdmin { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Blog { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Hireable { get; set; }
        public string Bio { get; set; }
        public string TwitterUsername { get; set; }
        public int PublicRepos { get; set; }
        public int PublicGists { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public override string ToString() =>
            $"{nameof(Login)}: {Login}\n" +
            $"{nameof(Name)}: {(string.IsNullOrEmpty(Name) ? "Unknown" : Name)}\n" +
            $"{nameof(Company)}: {(string.IsNullOrEmpty(Company) ? "Unknown" : Company)}\n" +
            $"{nameof(Blog)}: {(string.IsNullOrEmpty(Blog) ? "User Has No Blog" : Blog)}\n" +
            $"{nameof(Location)}: {(string.IsNullOrEmpty(Location) ? "Unknown" : Location)}\n" +
            $"{nameof(Email)}: {(string.IsNullOrEmpty(Email) ? "Unknown" : Email)}\n" +
            $"{nameof(Hireable)}: {(string.IsNullOrEmpty(Hireable) ? "False" : Hireable)}\n" +
            $"{nameof(Bio)}: {(string.IsNullOrEmpty(Bio) ? "Empty" : Bio)}\n" +
            $"{nameof(TwitterUsername)}: {(string.IsNullOrEmpty(TwitterUsername) ? "User Has No Twitter" : TwitterUsername)}\n" +
            $"{nameof(PublicRepos)}: {PublicRepos}\n" +
            $"Follower Count: {Followers}\n" +
            $"Following Count: {Following}\n" +
            $"{nameof(CreatedAt)}: {CreatedAt}\n" +
            $"{nameof(UpdatedAt)}: {UpdatedAt}";
    }
}