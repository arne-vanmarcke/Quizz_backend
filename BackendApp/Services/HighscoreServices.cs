using BackendApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BackendApp.Services
{
    public class HighscoreServices
    {
        private readonly IMongoCollection<Highscore> _HighscoreCollection;

        public HighscoreServices(
            IOptions<HighscoreDatabaseSettings> highscoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                highscoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                highscoreDatabaseSettings.Value.DatabaseName);

            _HighscoreCollection = mongoDatabase.GetCollection<Highscore>(
                highscoreDatabaseSettings.Value.HighscoreCollectionName);
        }

        public async Task<List<Highscore>> GetAsync() =>
            await _HighscoreCollection.Find(_ => true).ToListAsync();

        public async Task<Highscore?> GetAsync(string id) =>
            await _HighscoreCollection.Find(x => x.Id.ToString()==id).FirstOrDefaultAsync();

        public async Task CreateAsync(Highscore newHighscore) =>
            await _HighscoreCollection.InsertOneAsync(newHighscore);

        public async Task UpdateAsync(string id, Highscore updatedHighscore) =>
            await _HighscoreCollection.ReplaceOneAsync(x => x.Id.ToString() == id, updatedHighscore);

        public async Task RemoveAsync(string id) =>
            await _HighscoreCollection.DeleteOneAsync(x => x.Id.ToString() == id);
    }
}
