using System.Threading.Tasks;

namespace MMO_RPG_Console_Client
{
    public interface IHttpHandler
    {
        public Task<Player> CreatePlayer(string name);
        public Task<Player[]> GetAllPlayers();
        public Task<Player> GetPlayer(string player);
    }
}