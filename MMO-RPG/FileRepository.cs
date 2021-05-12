using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MMO_RPG.Model;
using Newtonsoft.Json;

namespace MMO_RPG
{
    public class FileRepository : IRepository
    {
        private string StoragePath => @"game-dev.txt";

        public async Task<Player> Get(Guid id)
        {
            var players = await GetAll();
            return players.FirstOrDefault(player => player.Id == id);
        }

        public async Task<List<Player>> GetAll()
        {
            var text = await File.ReadAllTextAsync(StoragePath);
            return JsonConvert.DeserializeObject<List<Player>>(text);
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
        
        public async Task<Player> AddItem(Guid id, NewItem item)
        {
            var players = await GetAll();
            foreach (var p in players)
            {
                if (p.Id != id)
                    continue;
                p.Inventory ??= new PlayerInventory();
                p.Inventory.AddItem(Item.CreateItem(item));
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

        public async Task DeleteItem(Guid id, Guid itemToDelete)
        {
            var players = await GetAll();
            foreach (var p in players)
            {
                if (p.Id != id)
                    continue;
                p.Inventory.Items.RemoveAll(item => item.Id == itemToDelete);
                var json = JsonConvert.SerializeObject(players);
                await File.WriteAllTextAsync(StoragePath, json);
                return;
            }
        }

        public async Task<PlayerInventory> ModifyItem(Guid id, Guid originalItem, ModifiedItem item)
        {
            var players = await GetAll();
            foreach (var p in players)
            {
                if (p.Id != id)
                    continue;
                var foundItem = p.Inventory.Items.Find(item1 => item1.Id.Equals(originalItem));
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