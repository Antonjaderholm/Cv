using Cv.Models;
using Cv.Data;
using Microsoft.Extensions.Logging;

namespace Cv.Services
{
    public class SkillService : ISkillService
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<SkillService> _logger;

        public SkillService(MongoDbContext context, ILogger<SkillService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Skill>> GetSkillsAsync()
        {
            Console.WriteLine("Fetching skills from MongoDB");
            return await _context.GetAllSkills("CV.Skills");
        }

        public async Task<Skill> GetSkillByIdAsync(string id)
        {
            _logger.LogInformation($"Fetching skill with ID: {id}");
            return await _context.GetSkillById("Skills", id);
        }

        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            _logger.LogInformation($"Adding new skill: {skill.Name}");
            var skills = await _context.AddSkill("Skills", skill);
            _logger.LogInformation($"Successfully added skill: {skill.Name}");
            return skill;
        }

        public async Task<Skill> UpdateSkillAsync(Skill skill)
        {
            _logger.LogInformation($"Updating skill: {skill.Name}");
            var skills = await _context.UpdateSkill("Skills", skill.Id, skill);
            _logger.LogInformation($"Successfully updated skill: {skill.Name}");
            return skill;
        }

        public async Task DeleteSkillAsync(string id)
        {
            _logger.LogInformation($"Deleting skill with ID: {id}");
            await _context.DeleteSkill("Skills", id);
            _logger.LogInformation($"Successfully deleted skill with ID: {id}");
        }
    }
}