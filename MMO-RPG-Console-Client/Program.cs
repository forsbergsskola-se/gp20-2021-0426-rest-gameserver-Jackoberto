using System;
using System.Threading.Tasks;
using MMO_RPG_Console_Client.Api;

namespace MMO_RPG_Console_Client
{
    class Program
    {
        private static IPlayerProvider createPlayer;
        private static IPlayerProvider getPlayer;
        private static ModifyPlayer modifyPlayer;
        private static Player player;

        static async Task Main(string[] args)
        {
            IHttpHandler httpHandler = new HttpHandler();
            createPlayer = new CreatePlayer(httpHandler);
            getPlayer = new GetPlayer(httpHandler);
            modifyPlayer = new ModifyPlayer(httpHandler);
            while (true)
            {
                await Run();
            }
        }

        static async Task Run()
        {
            Console.WriteLine("Do You Wanna Login Or Create A New Player \n" +
                              "1: Login \n" +
                              "2: Create \n");
            player = await GetPlayer();

            while (true)
            {
                player = await modifyPlayer.Run(player);
                if (player == null)
                    break;
            }
        }

        static async Task<Player> GetPlayer()
        {
            do
            {
                var input = Console.ReadLine();
                player = input switch
                {
                    "1" => await getPlayer.Run(),
                    "2" => await createPlayer.Run(),
                    _ => player
                };
            } while (player == null);

            return player;
        }
    }
}
