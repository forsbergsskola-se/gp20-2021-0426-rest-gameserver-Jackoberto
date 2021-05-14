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
    [Route("api/players/{playerId:guid}/items")]
    public class ItemController : ControllerBase
    {
        private readonly IRepository repository;
        
        public ItemController(IRepository repository)
        {
            this.repository = repository;
        }
        
        [HttpPost("new")]
        public async Task<Player> NewItem(Guid playerId, NewItem item)
        {
            var player = await repository.AddItem(playerId, item);
            return player;
        }
        
        [HttpGet("get-all")]
        public async Task<List<Item>> GetAll(Guid playerId)
        {
            var items = await repository.GetAllItems(playerId);
            return items.Items.Where(item => !item.IsDeleted).ToList();
        }

        [HttpPut("modify")]
        public async Task<List<Item>> Modify(Guid playerId, Guid originalItem, ModifiedItem modifiedItem)
        {
            var items = await repository.ModifyItem(playerId, originalItem, modifiedItem);
            return items.Items.Where(item => !item.IsDeleted).ToList();
        }

        [HttpDelete("delete")]
        public async Task Delete(Guid playerId, Guid item)
        {
            await repository.DeleteItem(playerId, item);
        }
    }
}
