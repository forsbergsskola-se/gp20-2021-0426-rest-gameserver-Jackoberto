﻿using System;

namespace MMO_RPG.Model
{
    public class Player
    {
        public Player()
        {
            
        }
        public Player(NewPlayer newPlayer)
        {
            Id = Guid.NewGuid();
            Name = newPlayer.Name;
            CreationTime = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}