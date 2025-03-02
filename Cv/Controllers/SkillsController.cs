using Microsoft.AspNetCore.Mvc;
using Cv.Services;
using Cv.Models;

namespace Cv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Skill>>> GetSkills()
        {
            var skills = await _skillService.GetSkillsAsync();
            return Ok(skills);
        }

        [HttpGet("test")]
        public ActionResult<string> Test()
        {
            return Ok("API is working!");
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> AddSkill([FromBody] Skill skill)
        {
            var result = await _skillService.AddSkillAsync(skill);
            return CreatedAtAction(nameof(GetSkills), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkill(string id)
        {
            await _skillService.DeleteSkillAsync(id);
            return NoContent();
        }
    }
}