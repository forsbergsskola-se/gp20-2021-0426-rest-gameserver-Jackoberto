using System;
using System.Threading.Tasks;

public interface IHttpHandler
{
    public Task<Player> CreatePlayer(string name);
    public Task<Player[]> GetAllPlayers();
}

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public int Level { get; set; }
    public DateTime CreationTime { get; set; }
}