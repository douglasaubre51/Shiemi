using MongoDB.Bson;

namespace Shiemi.Dto.Project
{
    public class ProjectDto
    {
        public string UserId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public Decimal128 Price { get; set; }
    }
}
