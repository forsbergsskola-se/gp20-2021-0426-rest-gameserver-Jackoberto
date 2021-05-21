using System;
using System.Threading.Tasks;

namespace MMO_RPG_Console_Client
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
                                  "1: Change Player Score\n");
                input = Console.ReadLine();
                if (input == "1")
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
            }
        }
    }
}