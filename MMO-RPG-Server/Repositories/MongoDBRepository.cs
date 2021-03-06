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
            var data = await collection.Find(filter).SingleOrDefaultAsync();
            return data;
        }

        public async Task<Player> Get(string name)
        {
            var collection = Database.GetCollection<Player>("players");
            var fieldDef = new StringFieldDefinition<Player, string>(nameof(Player.Name));
            var filter = Builders<Player>.Filter.Eq(fieldDef, name);
            var data = await collection.Find(filter).SingleOrDefaultAsync();
            return data;
        }

        public async Task<List<Player>> GetAll()
        {
            var collection = Database.GetCollection<Player>("players");
            var getDataTask = collection.Find(Builders<Player>.Filter.Eq("IsDeleted", false)).ToListAsync();
            return await getDataTask;
        }

        public async Task<Player> Create(NewPlayer newPlayer)
        {
            var collection = Database.GetCollection<Player>("players");
            var otherPlayer = await Get(newPlayer.Name);
            if (otherPlayer != null)
                throw new AlreadyExistException($"Player With Name {newPlayer.Name}");
            var player = Player.CreatePlayer(newPlayer);
            await collection.InsertOneAsync(player);
            return player;
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
        {
            var collection = Database.GetCollection<Player>("players");
            var fieldDef = new StringFieldDefinition<Player, Guid>(nameof(Player.Id));
            var filter = Builders<Player>.Filter.Eq(fieldDef, id);
            var update = Builders<Player>.Update.Set(nameof(Player.Score), modifiedPlayer.Score);
            var result = await collection.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<Player>{ ReturnDocument = ReturnDocument.After });
            return result;
        }
        
        public async Task<Player> AddItem(Guid id, NewItem item)
        {
            var collection = Database.GetCollection<Player>("players");
            var fieldDef = new StringFieldDefinition<Player, Guid>(nameof(Player.Id));
            var filter = Builders<Player>.Filter.Eq(fieldDef, id);
            var update = Builders<Player>.Update.Push($"{nameof(Player.Inventory)}.{nameof(PlayerInventory.Items)}", Item.CreateItem(item));
            var result = await collection.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<Player>{ ReturnDocument = ReturnDocument.After, IsUpsert = false});
            return result;
        }

        public async Task<PlayerInventory> GetAllItems(Guid id)
        {
            var player = await Get(id);
            return player.Inventory;
        }

        public async Task<Player> Delete(Guid id)
        {
            var collection = Database.GetCollection<Player>("players");
            var fieldDef = new StringFieldDefinition<Player, Guid>(nameof(Player.Id));
            var filter = Builders<Player>.Filter.Eq(fieldDef, id);
            var update = Builders<Player>.Update.Set(nameof(Player.IsDeleted), true);
            var result = await collection.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<Player>{ ReturnDocument = ReturnDocument.After });
            return result;
        }

        public async Task DeleteItem(Guid id, Guid itemToDelete)
        {
            var collection = Database.GetCollection<Player>("players");
            var fieldDef = new StringFieldDefinition<Player, Guid>(nameof(Player.Id));
            var elem = Builders<Player>.Filter.And(Builders<Player>.Filter.Eq(fieldDef, id), 
                Builders<Player>.Filter.ElemMatch(x => x.Inventory.Items, x => x.Id == itemToDelete));
            var update = Builders<Player>.Update.Set("Inventory.Items.$.IsDeleted", true);
            await collection.FindOneAndUpdateAsync(elem, update);
        }

        public async Task<PlayerInventory> ModifyItem(Guid id, Guid originalItem, ModifiedItem item)
        {
            var collection = Database.GetCollection<Player>("players");
            var fieldDef = new StringFieldDefinition<Player, Guid>(nameof(Player.Id));
            var elem = Builders<Player>.Filter.And(Builders<Player>.Filter.Eq(fieldDef, id), 
                Builders<Player>.Filter.ElemMatch(x => x.Inventory.Items, x => x.Id == originalItem));
            var update = Builders<Player>.Update.Set("Inventory.Items.$.Name", item.Name);
            var response = await collection.FindOneAndUpdateAsync(elem, update, new FindOneAndUpdateOptions<Player>{ ReturnDocument = ReturnDocument.After });
            return response.Inventory;
        }
    }
}