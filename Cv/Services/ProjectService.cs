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
            return await _context.GetAllProjects();
        }

        public async Task<Project> GetProjectByIdAsync(string id)
        {
            return await _context.GetProjectById(id);
        }

        public async Task<Project> AddProjectAsync(Project project)
        {
            var projects = await _context.AddProject(project);
            return projects.LastOrDefault();
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            var projects = await _context.UpdateProject(project.Id, project);
            return projects.FirstOrDefault(p => p.Id == project.Id);
        }

        public async Task DeleteProjectAsync(string id)
        {
            await _context.DeleteProject(id);
        }
    }
}