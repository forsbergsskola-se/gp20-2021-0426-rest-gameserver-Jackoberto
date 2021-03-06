using System;

namespace GitHubBrowser.Data
{
    public interface IOrganization : IGitHubJson
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public string NodeId { get; set; }
        public string Url { get; set; }
        public string ReposUrl { get; set; }
        public string EventsUrl { get; set; }
        public string HooksUrl { get; set; }
        public string IssuesUrl { get; set; }
        public string MembersUrl { get; set; }
        public string PublicMembersUrl { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Blog { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string TwitterUsername { get; set; }
        public bool IsVerified { get; set; }
        public bool HasOrganizationProjects { get; set; }
        public bool HasRepositoryProjects { get; set; }
        public int PublicRepos { get; set; }
        public int PublicGists { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public string HtmlUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Type { get; set; }
    }
}