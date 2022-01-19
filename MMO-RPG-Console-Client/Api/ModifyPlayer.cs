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
                                  "3: Add Item\n"+
                                  "4: Logout");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                    {
                        return await GetModifiedPlayer(player);
                    }
                    case "2":
                    {
                        Console.WriteLine(player.ToString());
                        break;
                    }
                    case "3":
                    {
                        return await AddItem(player);
                    }
                    case "4":
                    {
                        return null;
                    }
                }
            }
        }

        async Task<Player> GetModifiedPlayer(Player player)
        {
            ModifiedPlayer modifiedPlayer = null;
            do
            {
                Console.WriteLine("Write a score");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var result))
                {
                    modifiedPlayer = new ModifiedPlayer() {Score = result};
                }
            } while (modifiedPlayer == null);

            player = await HttpHandler.ModifyPlayer(modifiedPlayer, player.Id);
            return player;
        }
        async Task<Player> AddItem(Player player)
        {
            NewItem newItem = null;
            do
            {
                var level = 0;
                ItemType type;
                Console.WriteLine("Write a name for the item");
                var name = Console.ReadLine();
                Console.WriteLine("Write a level between 1-99");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var inputtedLevel) && inputtedLevel is <= 99 and >= 1)
                    level = inputtedLevel;
                else
                {
                    Console.WriteLine("Invalid Number");
                    continue;
                }
                    
                Console.WriteLine("Write a itemType between 0-4");
                input = Console.ReadLine();
                if (Enum.TryParse<ItemType>(input, out var inputtedType))
                    type = inputtedType;
                else
                {
                    Console.WriteLine("Invalid Type");
                    continue;
                }
                    
                newItem = new NewItem
                {
                    Name = name,
                    CreationTime = DateTime.Now,
                    Level = level,
                    Type = type
                };
            } while (newItem == null);

            player = await HttpHandler.AddItem(newItem, player.Id);
            return player;
        }
        
    }
}