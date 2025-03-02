using Cv.Models;
using Cv.Data;

namespace Cv.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjectsAsync();
        Task<Project> GetProjectByIdAsync(string id);
        Task<Project> AddProjectAsync(Project project);
        Task<Project> UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(string id);
    }
}

