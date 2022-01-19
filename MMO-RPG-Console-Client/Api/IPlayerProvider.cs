using System.Threading.Tasks;

namespace MMO_RPG_Console_Client.Api
{
    public interface IPlayerProvider
    {
        public Task<Player> Run();
    }
}