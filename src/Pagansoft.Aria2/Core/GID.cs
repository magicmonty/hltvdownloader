using System;
using System.Text.RegularExpressions;

namespace Pagansoft.Aria2.Core
{
    public class GID
    {
        public GID(string value)
        {
            if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, "^[0-9a-fA-F]{1,16}$"))
                throw new ArgumentException("GID must be a hex string of max 16 chars!");

            _value = value.PadLeft(16, '0');
        }

        public string Value { get { return _value; } }

        private readonly string _value;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (ReferenceEquals(null, obj))
                return false;
            
            var other = obj as GID;
            if (other == null)
            {
                var str = obj as string;
                if (str == null)
                    return false;
                
                other = new GID(str);
            }
            
            return Value == other.Value;
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
            return Equals(gid1, gid2);
        }

        public static bool operator !=(GID gid1, GID gid2)
        {
            return !(gid1 == gid2);
        }
    };
}
