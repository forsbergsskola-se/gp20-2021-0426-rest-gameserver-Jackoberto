using System;
using System.Threading.Tasks;

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
            var input = Console.ReadLine();
            if (input == "1")
            {
                player = await getPlayer.Run();
            }

            if (input == "2")
            {
                player = await createPlayer.Run();
            }

            while (true)
            {
                player = await modifyPlayer.Run(player);
                if (player == null)
                    break;
            }
        }
    }
}
