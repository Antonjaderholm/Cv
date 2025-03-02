using Cv.Models;
using Cv.Data;

namespace Cv.Services
{
    public class ProjectService : IProjectService
    {
        private readonly MongoDbContext _context;

        public ProjectService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _context.GetAllProjects("Projects");
        }

        public async Task<Project> GetProjectByIdAsync(string id)
        {
            return await _context.GetProjectById("Projects", id);
        }

        public async Task<Project> AddProjectAsync(Project project)
        {
            var projects = await _context.AddProject("Projects", project);
            return project;
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            var projects = await _context.UpdateProject("Projects", project.Id, project);
            return project;
        }

        public async Task DeleteProjectAsync(string id)
        {
            await _context.DeleteProject("Projects", id);
        }
    }
}