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
        
        [HttpPost("Modify")]
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
        
        [HttpGet("Get")]
        public async Task<Player> GetPlayer(Guid guid)
        {
            var player = await repository.Get(guid);
            return player;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Player>> GetAll()
        {
            var players = await repository.GetAll();
            return players;
        }
    }
}
