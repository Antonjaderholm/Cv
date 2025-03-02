using Cv.Models;
using Cv.Data;

namespace Cv.Services
{
    public interface ISkillService
    {
        Task<List<Skill>> GetSkillsAsync();
        Task<Skill> GetSkillByIdAsync(string id);
        Task<Skill> AddSkillAsync(Skill skill);
        Task<Skill> UpdateSkillAsync(Skill skill);
        Task DeleteSkillAsync(string id);
    }
}



