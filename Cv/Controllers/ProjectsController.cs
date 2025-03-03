using Microsoft.AspNetCore.Mvc;
using Cv.Services;
using Cv.Models;

namespace Cv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
            var projects = await _projectService.GetProjectsAsync();
            return Ok(projects);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> AddProject([FromBody] Project project)
        {
            var result = await _projectService.AddProjectAsync(project);
            return CreatedAtAction(nameof(GetProjects), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(string id)
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}