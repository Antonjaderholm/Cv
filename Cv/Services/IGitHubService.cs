using Cv.Models;
using Cv.Data; 

namespace CV.Services
{
    public interface IGitHubService
    {
        Task<IEnumerable<GitHubRepo>> GetUserRepositoriesAsync();
        Task<GitHubProfile> GetUserProfileAsync();
    }
}