using System;

namespace PaganSoft.Aria2.Core
{
    public class GID
    {
        public GID(string value)
        {
            _value = value;
        }

        public string Value { get { return _value; } }
        private readonly string _value;

        public override bool Equals(object obj)
        {
            var other = obj as GID;
            if ((object)null != (object)other) {
                return Value == other.Value;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(GID gid)
        {
            return gid.Value;
        }

        public static implicit operator GID(string gidValue)
        {
            return new GID(gidValue);
        }

        public static bool operator ==(GID gid1, GID gid2)
        {
            return (object)gid1 != (object)null && (object)gid2 != (object)null
                && gid1.Value == gid2.Value;
        }
        
        public static bool operator !=(GID gid1, GID gid2)
        {
            return (object)gid1 == (object)null || (object)gid2 == (object)null
                || gid1.Value != gid2.Value;
        }
    };
    
}
