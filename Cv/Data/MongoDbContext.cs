using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using Cv.Models;

namespace Cv.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Skill> _skills;
        private readonly IMongoCollection<Project> _projects;
        private readonly ILogger<MongoDbContext> _logger;

        public MongoDbContext(IMongoClient mongoClient, ILogger<MongoDbContext> logger)
        {
            _logger = logger;
            _database = mongoClient.GetDatabase("CV");
            _skills = _database.GetCollection<Skill>("Skills");
            _projects = _database.GetCollection<Project>("Projects");
            _logger.LogInformation("MongoDB connection established successfully");
        }

        // Skills CRUD
        public async Task<List<Skill>> AddSkill(string collection, Skill skill)
        {
            _logger.LogInformation($"Adding skill: {skill.Name}");
            try
            {
                await _skills.InsertOneAsync(skill);
                _logger.LogInformation("Skill added successfully");
                return await _skills.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding skill: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Skill>> GetAllSkills(string collection)
        {
            _logger.LogInformation("Fetching all skills");
            try
            {
                var skills = await _skills.Find(_ => true).ToListAsync();
                _logger.LogInformation($"Found {skills.Count} skills");
                return skills;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching skills: {ex.Message}");
                throw;
            }
        }

        public async Task<Skill> GetSkillById(string collection, string id)
        {
            _logger.LogInformation($"Fetching skill with ID: {id}");
            try
            {
                var skill = await _skills.Find(s => s.Id == id).FirstOrDefaultAsync();
                _logger.LogInformation(skill != null ? "Skill found" : "Skill not found");
                return skill;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching skill: {ex.Message}");
                throw;
            }
        }

        public async Task<string> DeleteSkill(string collection, string id)
        {
            _logger.LogInformation($"Deleting skill with ID: {id}");
            try
            {
                await _skills.DeleteOneAsync(s => s.Id == id);
                _logger.LogInformation("Skill deleted successfully");
                return "Skill deleted successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting skill: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Skill>> UpdateSkill(string collection, string id, Skill updatedSkill)
        {
            _logger.LogInformation($"Updating skill with ID: {id}");
            try
            {
                var update = Builders<Skill>.Update
                    .Set(s => s.Name, updatedSkill.Name)
                    .Set(s => s.YearsExperience, updatedSkill.YearsExperience)
                    .Set(s => s.SkillLevel, updatedSkill.SkillLevel);
                await _skills.UpdateOneAsync(s => s.Id == id, update);
                _logger.LogInformation("Skill updated successfully");
                return await _skills.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating skill: {ex.Message}");
                throw;
            }
        }

        // Projects CRUD
        public async Task<List<Project>> AddProject(string collection, Project project)
        {
            _logger.LogInformation($"Adding project: {project.Title}");
            try
            {
                await _projects.InsertOneAsync(project);
                _logger.LogInformation("Project added successfully");
                return await _projects.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding project: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Project>> GetAllProjects(string collection)
        {
            _logger.LogInformation("Fetching all projects");
            try
            {
                var projects = await _projects.Find(_ => true).ToListAsync();
                _logger.LogInformation($"Found {projects.Count} projects");
                return projects;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching projects: {ex.Message}");
                throw;
            }
        }

        public async Task<Project> GetProjectById(string collection, string id)
        {
            _logger.LogInformation($"Fetching project with ID: {id}");
            try
            {
                var project = await _projects.Find(p => p.Id == id).FirstOrDefaultAsync();
                _logger.LogInformation(project != null ? "Project found" : "Project not found");
                return project;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching project: {ex.Message}");
                throw;
            }
        }

        public async Task<string> DeleteProject(string collection, string id)
        {
            _logger.LogInformation($"Deleting project with ID: {id}");
            try
            {
                await _projects.DeleteOneAsync(p => p.Id == id);
                _logger.LogInformation("Project deleted successfully");
                return "Project deleted successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting project: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Project>> UpdateProject(string collection, string id, Project project)
        {
            _logger.LogInformation($"Updating project with ID: {id}");
            try
            {
                await _projects.ReplaceOneAsync(p => p.Id == id, project);
                _logger.LogInformation("Project updated successfully");
                return await _projects.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating project: {ex.Message}");
                throw;
            }
        }
    }
}