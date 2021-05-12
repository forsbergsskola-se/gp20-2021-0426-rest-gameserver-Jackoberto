using System;

namespace MMO_RPG.Model
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
                CreationTime = DateTime.Now
            };
            return item;
        }
        
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime CreationTime { get; set; }
    }
}