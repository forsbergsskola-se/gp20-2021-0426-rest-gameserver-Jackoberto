using System;
using System.ComponentModel.DataAnnotations;

namespace MMO_RPG_Console_Client
{
    public class NewItem
    {
        public string Name { get; set; }
        
        public int Level { get; set; }
        
        public ItemType Type { get; set; }
        
        public DateTime CreationTime { get; set; }
    }
}