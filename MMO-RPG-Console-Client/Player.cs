using System;

namespace MMO_RPG_Console_Client
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public DateTime CreationTime { get; set; }
        
        public PlayerInventory Inventory { get; set; } = new ();

        public override string ToString() => $"{nameof(Id)}: {Id}\n" +
                                             $"{nameof(Name)}: {Name}\n" +
                                             $"{nameof(Score)}: {Score}\n" +
                                             $"{nameof(Level)}: {Level}\n" +
                                             $"{nameof(CreationTime)}: {CreationTime}\n"+
                                             $"{nameof(Inventory)}: {Inventory}";
    }
}