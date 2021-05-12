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
    [Route("[controller]/players/{playerId:guid}/items")]
    public class ItemController : ControllerBase
    {
        private readonly IRepository repository;
        
        public ItemController(IRepository repository)
        {
            this.repository = repository;
        }
        
        [HttpPost("New")]
        public async Task<Player> NewItem(Guid playerId, NewItem item)
        {
            var player = await repository.AddItem(playerId, item);
            return player;
        }
        
        [HttpGet("GetAll")]
        public async Task<List<Item>> GetAll(Guid playerId)
        {
            var items = await repository.GetAllItems(playerId);
            return items.Items.Where(item => !item.IsDeleted).ToList();
        }

        [HttpPost("Modify")]
        public async Task<PlayerInventory> Modify(Guid playerId, Guid originalItem, ModifiedItem item)
        {
            var items = await repository.ModifyItem(playerId, originalItem, item);
            return items;
        }

        [HttpDelete("Delete")]
        public async Task Delete(Guid playerId, Guid item)
        {
            await repository.DeleteItem(playerId, item);
        }
    }
}
