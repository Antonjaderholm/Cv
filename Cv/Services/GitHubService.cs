using Cv.Models;
using Cv.Data;
using Cv.Services;

namespace CV.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;
        private const string USERNAME = "Antonjaderholm";

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CV-Application");
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<GitHubRepo>> GetUserRepositoriesAsync()
        {
            var repos = await _httpClient.GetFromJsonAsync<List<GitHubRepo>>($"https://api.github.com/users/{USERNAME}/repos");
            return repos ?? Enumerable.Empty<GitHubRepo>();
        }

        public async Task<GitHubProfile> GetUserProfileAsync()
        {
            var profile = await _httpClient.GetFromJsonAsync<GitHubProfile>($"https://api.github.com/users/{USERNAME}");
            return profile ?? new GitHubProfile();
        }
    }
}