using MongoDB.Bson;
using MongoDB.Driver;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Repository
{
    public class MongoDbSuperHeroRepository : ISuperHeroRepository
    {
        private const string dbName = "superheroes";
        private const string collectionName = "superhero";
        private readonly IMongoCollection<SuperHero> _superHeroCollection;
        private readonly FilterDefinitionBuilder<SuperHero> filterBuilder = Builders<SuperHero>.Filter;

        public MongoDbSuperHeroRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            _superHeroCollection = database.GetCollection<SuperHero>(collectionName);
        }

        public async Task AddSuperHeroAsync(SuperHero hero)
        {
            await _superHeroCollection.InsertOneAsync(hero);
        }

        public async Task DeleteSuperHeroAsync(Guid id)
        {
            var filter = filterBuilder.Eq(hero => hero.Id, id);
            await _superHeroCollection.DeleteOneAsync(filter);
        }

        public async Task<SuperHero> GetSuperHeroAsync(Guid id)
        {
            var filter = filterBuilder.Eq(hero => hero.Id, id);
            return await _superHeroCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<SuperHero>> GetSuperHeroesAsync()
        {
            return await _superHeroCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateSuperHeroAsync(SuperHero hero)
        {
            var filter = filterBuilder.Eq(existingHero => existingHero.Id, hero.Id);
            await _superHeroCollection.ReplaceOneAsync(filter, hero);
        }
    }
}