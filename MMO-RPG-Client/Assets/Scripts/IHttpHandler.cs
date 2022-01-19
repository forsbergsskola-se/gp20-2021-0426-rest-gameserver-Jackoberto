using System.Threading.Tasks;

public interface IHttpHandler
{
    public Task<Player> CreatePlayer(string name);
    public Task<Player[]> GetAllPlayers();
    public Task<Player> GetPlayer(string player);
}