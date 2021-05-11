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

        public string Name { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}