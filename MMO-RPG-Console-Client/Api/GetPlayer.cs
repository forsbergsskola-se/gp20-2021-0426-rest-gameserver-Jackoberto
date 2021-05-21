using System;
using System.Linq;
using System.Threading.Tasks;

namespace MMO_RPG_Console_Client.Api
{
    public class GetPlayer : IPlayerProvider
    {
        public GetPlayer(IHttpHandler httpHandler)
        {
            HttpHandler = httpHandler;
        }
        private IHttpHandler HttpHandler { get; }

        public async Task<Player> Run()
        {
            Player player = null;
            do
            {
                Console.WriteLine("Do you wanna \n" +
                                  "1: Login with a Name \n" +
                                  "2: See all players");
                var input = Console.ReadLine();
                if (input == "1")
                {
                    player = await Get();
                }

                if (input == "2")
                {
                    await GetAll();
                }
            } while (player == null);

            return player;
        }

        private async Task<Player> Get()
        {
            Console.WriteLine("Type Your Name Or ID");
            Player player;
            do
            {
                var input = Console.ReadLine();
                player = await HttpHandler.GetPlayer(input);
                if (player == null)
                {
                    Console.WriteLine($"{input} is an invalid name/id");
                }
            } while (player == null);

            return player;
        }
    
        private async Task GetAll()
        {
            var players = await HttpHandler.GetAllPlayers();
            var s = players.Aggregate(string.Empty, (current, player) => current + player.Name + "\n");
            Console.WriteLine(s);
        }
    }
}
