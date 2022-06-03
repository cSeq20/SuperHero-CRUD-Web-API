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

        public void AddSuperHero(SuperHero hero)
        {
            _superHeroCollection.InsertOne(hero);
        }

        public void DeleteSuperHero(Guid id)
        {
            var filter = filterBuilder.Eq(hero => hero.Id, id);
            _superHeroCollection.DeleteOne(filter);
        }

        public SuperHero GetSuperHero(Guid id)
        {
            var filter = filterBuilder.Eq(hero => hero.Id, id);
            return _superHeroCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<SuperHero> GetSuperHeroes()
        {
            return _superHeroCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateSuperHero(SuperHero hero)
        {
            var filter = filterBuilder.Eq(existingHero => existingHero.Id, hero.Id);
            _superHeroCollection.ReplaceOne(filter, hero);
        }
    }
}
