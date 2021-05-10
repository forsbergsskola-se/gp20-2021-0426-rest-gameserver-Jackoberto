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
            var players = await GetAll();
            return players.FirstOrDefault(player => player.Id == id);
        }

        public async Task<List<Player>> GetAll()
        {
            var text = Database.GetCollection<BsonDocument>("players");
            var data = await text.Find(FilterDefinition<BsonDocument>.Empty).ToListAsync();
            return await Task.Run(() => InternalGetAll(data));
        }

        private List<Player> InternalGetAll(List<BsonDocument> bsonDocuments)
        {
            return bsonDocuments.Select(bsonDocument => BsonSerializer.Deserialize<Player>(bsonDocument)).ToList();
        }

        public async Task<Player> Create(NewPlayer newPlayer)
        {
            var players = await GetAll();
            var list = players.ToList();
            var addedPlayer = Player.CreatePlayer(newPlayer);
            list.Add(addedPlayer);
            var json = JsonConvert.SerializeObject(players);
            await File.WriteAllTextAsync(StoragePath, json);
            return addedPlayer;
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
        {
            var players = await GetAll();
            foreach (var p in players)
            {
                if (p.Id != id)
                    continue;
                p.Score = modifiedPlayer.Score;
                var json = JsonConvert.SerializeObject(players);
                await File.WriteAllTextAsync(StoragePath, json);
                return p;
            }

            return null;
        }
        
        public async Task<Player> AddItem(Guid id, Item item)
        {
            var players = await GetAll();
            foreach (var p in players)
            {
                if (p.Id != id)
                    continue;
                p.Inventory ??= new PlayerInventory();
                p.Inventory.AddItem(item);
                var json = JsonConvert.SerializeObject(players);
                await File.WriteAllTextAsync(StoragePath, json);
                return p;
            }

            return null;
        }

        public async Task<PlayerInventory> GetAllItems(Guid id)
        {
            var player = await Get(id);
            return player.Inventory;
        }

        public async Task<Player> Delete(Guid id)
        {
            var players = await GetAll();
            foreach (var p in players)
            {
                if (p.Id != id)
                    continue;
                p.IsDeleted = true;
                var json = JsonConvert.SerializeObject(players);
                await File.WriteAllTextAsync(StoragePath, json);
                return p;
            }

            return null;
        }

        public async Task DeleteItem(Guid id, Item item)
        {
            var players = await GetAll();
            foreach (var p in players)
            {
                if (p.Id != id)
                    continue;
                p.Inventory.Items.Remove(item);
                var json = JsonConvert.SerializeObject(players);
                await File.WriteAllTextAsync(StoragePath, json);
                return;
            }
        }

        public async Task<PlayerInventory> ModifyItem(Guid id, string originalItem, ModifiedItem item)
        {
            var players = await GetAll();
            foreach (var p in players)
            {
                if (p.Id != id)
                    continue;
                var foundItem = p.Inventory.Items.Find(item1 => item1.Name.Equals(originalItem));
                if (foundItem == null)
                    return p.Inventory;
                foundItem.Name = item.Name;
                var json = JsonConvert.SerializeObject(players);
                await File.WriteAllTextAsync(StoragePath, json);
                return p.Inventory;
            }

            return null;
        }
    }
}