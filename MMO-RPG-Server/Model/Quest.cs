using System;

namespace MMO_RPG.Model
{
    public class Quest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
    }
}