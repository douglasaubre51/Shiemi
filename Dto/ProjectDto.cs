namespace Shiemi.Dto
{
    public class ProjectDto
    {
        public string UserId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }

        public DateOnly EndsAt { get; set; }
    }
}
