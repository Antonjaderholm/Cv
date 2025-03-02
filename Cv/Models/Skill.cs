using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cv.Models
{
    public class Skill
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Technology")]
        public string Name { get; set; }

        public int SkillLevel { get; set; }

        [BsonElement("YearsOfExperience")]
        public int YearsExperience { get; set; }
    }
}