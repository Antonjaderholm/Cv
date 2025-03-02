using Cv.Data;
using Cv.Models;
using Cv.Services;

public class SkillService : ISkillService
{
    private readonly MongoDbContext _context;

    public SkillService(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<List<Skill>> GetSkillsAsync()
    {
        return await _context.GetAllSkills();
    }

    public async Task<Skill> GetSkillByIdAsync(string id)
    {
        return await _context.GetSkillById(id);
    }

    public async Task<Skill> AddSkillAsync(Skill skill)
    {
        var skills = await _context.AddSkill(skill);
        return skills.LastOrDefault();
    }

    public async Task<Skill> UpdateSkillAsync(Skill skill)
    {
        var skills = await _context.UpdateSkill(skill.Id, skill);
        return skills.FirstOrDefault(s => s.Id == skill.Id);
    }

    public async Task DeleteSkillAsync(string id)
    {
        await _context.DeleteSkill(id);
    }
}