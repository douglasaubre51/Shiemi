using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shiemi.Models
{
    public class ProjectModel
    {
        [BsonElement("_id")]
        public string? ProjectId { get; set; }
        [BsonElement("__v")]
        public int Version { get; set; }

        [BsonElement("userId")]
        public string? UserId { get; set; }
        [BsonElement("channelId")]
        public string? ChannelId { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }
        [BsonElement("shortDescription")]
        public string? ShortDescription { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("image")]
        public string? Image { get; set; }

        [BsonElement("price")]
        public Decimal128 Price { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
