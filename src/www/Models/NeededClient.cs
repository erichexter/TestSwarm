using System;

namespace nTestSwarm.Models
{
    public class NeededClient : IEquatable<NeededClient>
    {
        public string Browser { get; set; }

        public int? Version { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as NeededClient);
        }

        public bool Equals(NeededClient other)
        {
            if (other == null) return false;

            if (object.ReferenceEquals(this, other)) return true;

            return Browser == other.Browser && Version == other.Version;
        }

        public override int GetHashCode()
        {
            int browserHash = Browser == null ? 0 : Browser.GetHashCode();
            int versionHash = Version.HasValue ? Version.GetHashCode() : 0;

            return browserHash ^ versionHash;
        }
    }
}