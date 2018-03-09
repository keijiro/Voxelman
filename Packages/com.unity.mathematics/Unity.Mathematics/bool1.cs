using System;

namespace Unity.Mathematics
{
    /// <summary>
    /// A bool compatible for marshalling with native code.
    /// </summary>
    public struct bool1 : IEquatable<bool1>
    {
        private readonly int _value;

        public bool1(bool value)
        {
            this._value = value ? 1 : 0;
        }

        public static implicit operator bool(bool1 value)
        {
            return value._value != 0;
        }

        public static implicit operator bool1(bool value)
        {
            return new bool1(value);
        }

        public bool Equals(bool1 other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is bool1 && Equals((bool1)obj);
        }

        public override int GetHashCode()
        {
            return _value;
        }

        public static bool operator ==(bool1 left, bool1 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(bool1 left, bool1 right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return ((bool)this).ToString();
        }
    }
}
