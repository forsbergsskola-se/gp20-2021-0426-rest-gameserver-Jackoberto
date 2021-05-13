using System;
using System.ComponentModel.DataAnnotations;

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
                Level = newItem.Level,
                Type = newItem.Type,
                CreationTime = DateTime.Now
            };
            return item;
        }
        
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public bool IsDeleted { get; set; }
        
        [Range(1, 99)]
        public int Level { get; set; }
        
        [EnumDataType(typeof(ItemType))]
        public ItemType Type { get; set; }
        
        [DateValidation]
        public DateTime CreationTime { get; set; }
    }
}