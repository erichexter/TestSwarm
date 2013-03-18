using System;

namespace nTestSwarm.Application.Queries
{
    public class Descriptor : IEquatable<Descriptor>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }

        public bool Equals(Descriptor other)
        {
            if (other == null) 
                return false;
            if (object.ReferenceEquals(other, this)) 
                return true;

            return Id == other.Id && Name == other.Name && Selected == other.Selected;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Descriptor);
        }

        public override int GetHashCode()
        {
            int idHashCode = Id.GetHashCode();
            int nameHashCode = Name == null ? 0 : Name.GetHashCode();
            int selectedHashCode = Selected.GetHashCode();

            return idHashCode ^ nameHashCode ^ selectedHashCode;
        }
    }
}