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
    [Route("[controller]")]
    public class RepositoryController : ControllerBase
    {
        private readonly ILogger<RepositoryController> _logger;
        private readonly IRepository repository;
        
        public RepositoryController(ILogger<RepositoryController> logger)
        {
            repository = new FileRepository();
            _logger = logger;
        }
        
        [HttpPost("SetPlayer")]
        public async Task<Player> SetPlayer(Player player)
        {
            var newPlayer = await repository.Create(player);
            return newPlayer;
        }

        [HttpGet("GetPlayers")]
        public async Task<IEnumerable<Player>> GetAll()
        {
            var players = await repository.GetAll();
            return players;
        }
    }
}
