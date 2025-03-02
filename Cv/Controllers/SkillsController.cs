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
            try
            {
                var skills = await _skillService.GetSkillsAsync();
                return Ok(skills);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("test")]
        public ActionResult<string> Test()
        {
            return Ok("API is working!");
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> AddSkill([FromBody] Skill skill)
        {
            try
            {
                var result = await _skillService.AddSkillAsync(skill);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkill(string id)
        {
            try
            {
                await _skillService.DeleteSkillAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}