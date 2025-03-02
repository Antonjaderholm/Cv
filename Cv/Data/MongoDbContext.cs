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
        public async Task<List<Skill>> AddSkill(Skill skill)
        {
            _logger.LogInformation($"Adding skill: {skill.Name}");
            await _skills.InsertOneAsync(skill);
            return await _skills.Find(_ => true).ToListAsync();
        }

        public async Task<List<Skill>> GetAllSkills()
        {
            var skills = await _skills.Find(_ => true).ToListAsync();
            _logger.LogInformation($"Found {skills.Count} skills");
            return skills;
        }

        public async Task<Skill> GetSkillById(string id)
        {
            return await _skills.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> DeleteSkill(string id)
        {
            await _skills.DeleteOneAsync(s => s.Id == id);
            return "Skill deleted successfully";
        }

        public async Task<List<Skill>> UpdateSkill(string id, Skill updatedSkill)
        {
            var update = Builders<Skill>.Update
                .Set(s => s.Name, updatedSkill.Name)
                .Set(s => s.YearsExperience, updatedSkill.YearsExperience)
                .Set(s => s.SkillLevel, updatedSkill.SkillLevel);
            await _skills.UpdateOneAsync(s => s.Id == id, update);
            return await _skills.Find(_ => true).ToListAsync();
        }

        // Projects CRUD
        public async Task<List<Project>> AddProject(Project project)
        {
            await _projects.InsertOneAsync(project);
            return await _projects.Find(_ => true).ToListAsync();
        }

        public async Task<List<Project>> GetAllProjects()
        {
            var projects = await _projects.Find(_ => true).ToListAsync();
            _logger.LogInformation($"Found {projects.Count} projects");
            return projects;
        }

        public async Task<Project> GetProjectById(string id)
        {
            return await _projects.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> DeleteProject(string id)
        {
            await _projects.DeleteOneAsync(p => p.Id == id);
            return "Project deleted successfully";
        }

        public async Task<List<Project>> UpdateProject(string id, Project project)
        {
            await _projects.ReplaceOneAsync(p => p.Id == id, project);
            return await _projects.Find(_ => true).ToListAsync();
        }
    }
}