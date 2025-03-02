using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cv.Models
{
    public class Skill
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int YearsExperience { get; set; }
    }
}