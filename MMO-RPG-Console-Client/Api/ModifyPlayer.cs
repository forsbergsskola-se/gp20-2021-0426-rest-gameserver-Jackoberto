using System;
using System.Threading.Tasks;

namespace MMO_RPG_Console_Client.Api
{
    public class ModifyPlayer
    {
        public ModifyPlayer(IHttpHandler httpHandler)
        {
            HttpHandler = httpHandler;
        }

        private IHttpHandler HttpHandler { get; }

        public async Task<Player> Run(Player player)
        {
            var input = string.Empty;
            while (true)
            {
                Console.WriteLine("Do You Wanna\n" +
                                  "1: Change Player Score\n" +
                                  "2: Get Player Info\n"+
                                  "3: Logout");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                    {
                        ModifiedPlayer modifiedPlayer = null;
                        do
                        {
                            Console.WriteLine("Write a score");
                            input = Console.ReadLine();
                            if (int.TryParse(input, out var result))
                            {
                                modifiedPlayer = new ModifiedPlayer() {Score = result};
                            }
                        } while (modifiedPlayer == null);

                        player = await HttpHandler.ModifyPlayer(modifiedPlayer, player.Id);
                        return player;
                    }
                    case "2":
                    {
                        Console.WriteLine(player.ToString());
                        break;
                    }
                    case "3":
                    {
                        return null;
                    }
                }
            }
        }
    }
}