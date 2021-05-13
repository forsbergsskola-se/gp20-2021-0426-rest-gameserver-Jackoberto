using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMO_RPG.Model;
using Newtonsoft.Json;

namespace MMO_RPG.Controllers
{
    [ApiController]
    [Route("[controller]/players")]
    public class PlayerController : ControllerBase
    {
        private readonly IRepository repository;
        
        public PlayerController(IRepository repository)
        {
            this.repository = repository;
        }
        
        [HttpPost("New")]
        public async Task<Player> NewPlayer(NewPlayer player)
        {
            var newPlayer = await repository.Create(player);
            return newPlayer;
        }
        
        [HttpPut("Modify")]
        public async Task<Player> ModifyPlayer(Guid guid, ModifiedPlayer modifiedPlayer)
        {
            var newPlayer = await repository.Modify(guid, modifiedPlayer);
            return newPlayer;
        }
        
        [HttpDelete("Delete")]
        public async Task<Player> DeletePlayer(Guid guid)
        {
            var player = await repository.Delete(guid);
            return player;
        }
        
        [HttpGet("Get/{id:guid}")]
        public async Task<Player> GetPlayer(Guid id)
        {
            var player = await repository.Get(id);
            return player;
        }
        
        [HttpGet("Get/{name}")]
        public async Task<Player> GetPlayer(string name)
        {
            var player = await repository.Get(name);
            return player;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Player>> GetAll(int minScore)
        {
            var players = await repository.GetAll();
            return players.Where(player => player.Score >= minScore);
        }
    }
}
