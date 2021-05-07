using System;
using System.Threading.Tasks;
using MMO_RPG.Model;

namespace MMO_RPG
{
    public interface IRepository
    {
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer);
        Task<Player> Delete(Guid id);
    }
}