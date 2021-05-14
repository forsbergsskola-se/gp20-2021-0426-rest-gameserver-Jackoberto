using System.Collections.Generic;

namespace MMO_RPG.Model
{
    public class PlayerInventory
    {
        public List<Item> Items { get; set; } = new();

        public void AddItem(Item item)
        {
            Items ??= new List<Item>();
            Items.Add(item);
        }
    }
}