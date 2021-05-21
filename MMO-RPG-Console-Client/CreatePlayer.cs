using System;
using System.Threading.Tasks;

namespace MMO_RPG_Console_Client
{
    public class CreatePlayer : IPlayerProvider
    {
        public CreatePlayer(IHttpHandler httpHandler)
        {
            HttpHandler = httpHandler;
        }
        private IHttpHandler HttpHandler { get; }

        public async Task<Player> Run()
        {
            Console.WriteLine("Write Your Name");
            Player player;
            do
            {
                player = await Create(Console.ReadLine());
                if (player == null)
                {
                    Console.WriteLine("Name was already taken or another error occured try again");
                }
            } while (player == null);

            return player;
        }

        private async Task<Player> Create(string name)
        {
            return await HttpHandler.CreatePlayer(new NewPlayer{Name = name});
        }
    }

    public interface IPlayerProvider
    {
        public Task<Player> Run();
    }
}