using System;
namespace MMO_RPG_Console_Client
{
    public class Item
    {
        protected bool Equals(Item other) => Name == other.Name;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Item) obj);
        }

        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

        public static Item CreateItem(NewItem newItem)
        {
            var item = new Item
            {
                Name = newItem.Name,
                Id = Guid.NewGuid(),
                Level = newItem.Level,
                Type = newItem.Type,
                CreationTime = DateTime.Now
            };
            return item;
        }
        
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public int Level { get; set; }
        public ItemType Type { get; set; }
        
        public DateTime CreationTime { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} : Type {Type}";
        }
    }
}