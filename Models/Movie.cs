namespace first_asp_dot_net_project.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    public class Movie
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string Summary { get; set; }

        public List<string> Actors { get; set; }
    }
}
