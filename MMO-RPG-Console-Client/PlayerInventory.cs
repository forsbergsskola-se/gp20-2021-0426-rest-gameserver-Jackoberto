using System.Collections.Generic;
using System.Linq;

namespace MMO_RPG_Console_Client
{
    public class PlayerInventory
    {
        public List<Item> Items { get; set; } = new();

        public override string ToString()
        {
            return Items.Aggregate("\n", (current, item) => current + $"    {item}\n");
        }
    }
}