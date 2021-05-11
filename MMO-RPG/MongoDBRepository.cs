using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MMO_RPG.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MMO_RPG
{
    public class MongoDBRepository : IRepository
    {
        private const string StoragePath = @"mmo-game";

        public MongoDBRepository()
        {
            var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true), new NoIdMemberConvention() };
            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);
        }

        private IMongoDatabase Database { get; init; } = new MongoClient("mongodb://localhost:27017").GetDatabase(StoragePath);

        public async Task<Player> Get(Guid id)
        {
            var collection = Database.GetCollection<Player>("players");
            var fieldDef = new StringFieldDefinition<Player, Guid>(nameof(Player.Id));
            var filter = Builders<Player>.Filter.Eq(fieldDef, id);
            var data = await collection.Find(filter).ToListAsync();
            var document = data.FirstOrDefault();
            if (document is null)
                throw new Exception($"No Player Was Found With This GUID {id}");
            return document;
        }

        public async Task<List<Player>> GetAll()
        {
            var collection = Database.GetCollection<Player>("players");
            var getDataTask = collection.Find(FilterDefinition<Player>.Empty).ToListAsync();
            return await getDataTask;
        }

        public async Task<Player> Create(NewPlayer newPlayer)
        {
            var collection = Database.GetCollection<Player>("players");
            var player = Player.CreatePlayer(newPlayer);
            await collection.InsertOneAsync(player);
            return player;
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
        {
            throw new NotImplementedException();
            var collection = Database.GetCollection<Player>("players");
            var fieldDef = new StringFieldDefinition<Player, Guid>(nameof(Player.Id));
            var filter = Builders<Player>.Filter.Eq(fieldDef, id);
            var update = new BsonDocument("$inc", new BsonDocument( {{"Score", modifiedPlayer.Score}});
            var result = await collection.UpdateOneAsync(filter, update);
        }
        
        public async Task<Player> AddItem(Guid id, Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerInventory> GetAllItems(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Player> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteItem(Guid id, Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerInventory> ModifyItem(Guid id, string originalItem, ModifiedItem item)
        {
            throw new NotImplementedException();
        }
    }
}