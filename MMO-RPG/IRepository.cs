using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMO_RPG.Model;

namespace MMO_RPG
{
    public interface IRepository
    {
        Task<Player> Get(Guid id);
        Task<List<Player>> GetAll();
        Task<Player> Create(NewPlayer newPlayer);
        Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer);
        Task<Player> AddItem(Guid id, Item item);
        Task<PlayerInventory> GetAllItems(Guid id);
        Task<Player> Delete(Guid id);
        Task DeleteItem(Guid id, Item item);
        Task<PlayerInventory> ModifyItem(Guid id, string originalItem, ModifiedItem item);
    }
}