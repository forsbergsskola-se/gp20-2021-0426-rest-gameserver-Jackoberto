using System;

namespace MMO_RPG.Model
{
    public class Player
    {
        public static Player CreatePlayer(NewPlayer newPlayer)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                Name = newPlayer.Name,
                CreationTime = DateTime.UtcNow
            };
            return player;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public PlayerInventory Inventory { get; set; } = new ();
    }
}