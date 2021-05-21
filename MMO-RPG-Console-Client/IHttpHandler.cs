﻿using System;
using System.Threading.Tasks;

namespace MMO_RPG_Console_Client
{
    public interface IHttpHandler
    {
        public Task<Player> CreatePlayer(NewPlayer player);
        public Task<Player[]> GetAllPlayers();
        public Task<Player> GetPlayer(string player);
        public Task<Player> ModifyPlayer(ModifiedPlayer player, Guid guid);
    }
}