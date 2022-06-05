namespace first_asp_dot_net_project.Controllers
{
    using first_asp_dot_net_project.Data;
    using first_asp_dot_net_project.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class MoviesController
    {
        private readonly IMongoCollection<Movie> _movies;

        public MoviesController(IOptions<DatabaseSettings> options)
        {
            MongoClient mongoClient = new MongoClient(options.Value.ConnectionString);

            this._movies = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<Movie>(options.Value.CollectionName);
        }

        public async Task<List<Movie>> Get() => 
            await this._movies.Find(_ => true).ToListAsync();

        public async Task<Movie> Get(string id) => 
            await this._movies.Find(m => m.Id == id).FirstOrDefaultAsync();

        public async Task Create(Movie movie) =>
            await this._movies.InsertOneAsync(movie);

        public async Task Update(string id, Movie movie) =>
            await this._movies.ReplaceOneAsync(m => m.Id == id, movie);

        public async Task Delete(string id) =>
            await this._movies.DeleteOneAsync(m => m.Id == id);
    }
}
