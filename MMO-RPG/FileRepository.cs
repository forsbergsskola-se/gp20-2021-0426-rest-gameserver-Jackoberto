﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MMO_RPG.Model;
using Newtonsoft.Json;

namespace MMO_RPG
{
    public class FileRepository : IRepository
    {
        private string StoragePath => "game-dev.txt";

        public async Task<Player> Get(Guid id)
        {
            var players = await GetAll();
            return players.FirstOrDefault(player => player.Id == id);
        }

        public async Task<Player[]> GetAll()
        {
            var text = await File.ReadAllTextAsync(StoragePath);
            return JsonConvert.DeserializeObject<Player[]>(text);
        }

        public async Task<Player> Create(Player player)
        {
            var players = await GetAll();
            var list = players.ToList();
            list.Add(player);
            players = list.ToArray();
            var json = JsonConvert.SerializeObject(players);
            await File.WriteAllTextAsync(StoragePath, json);
            return player;
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

        public async Task<Player> Delete(Guid id)
        {
            var players = await GetAll();
            foreach (var p in players)
            {
                if (p.Id != id)
                    continue;
                p.IsDeleted = true;
                var list = players.ToList();
                list.Remove(p);
                var modifiedList = list.ToArray();
                var json = JsonConvert.SerializeObject(modifiedList);
                await File.WriteAllTextAsync(StoragePath, json);
                return p;
            }

            return null;
        }
    }
}