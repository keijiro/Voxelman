using System;

namespace Unity.Entities
{
    public struct SubtractiveComponent<T> where T : struct, IComponentData
    {}

    public struct ComponentType
    {
        public enum AccessMode
        {
            ReadWrite,
            ReadOnly,
            Subtractive
        }

        public int            TypeIndex;
        public AccessMode     AccessModeType;
        public int            FixedArrayLength;

        public static ComponentType Create<T>()
        {
            ComponentType type;
            type.TypeIndex = TypeManager.GetTypeIndex<T>();
            type.AccessModeType = AccessMode.ReadWrite;
            type.FixedArrayLength = -1;
            return type;
        }

        public static ComponentType ReadOnly(Type type)
        {
            ComponentType t;
            t.TypeIndex = TypeManager.GetTypeIndex(type);
            t.AccessModeType = AccessMode.ReadOnly;
            t.FixedArrayLength = -1;
            return t;
        }
        public static ComponentType ReadOnly<T>()
        {
            ComponentType t;
            t.TypeIndex = TypeManager.GetTypeIndex<T>();
            t.AccessModeType = AccessMode.ReadOnly;
            t.FixedArrayLength = -1;
            return t;
        }

        public static ComponentType Subtractive(Type type)
        {
            ComponentType t;
            t.TypeIndex = TypeManager.GetTypeIndex(type);
            t.AccessModeType = AccessMode.Subtractive;
            t.FixedArrayLength = -1;
            return t;
        }
        public static ComponentType Subtractive<T>()
        {
            ComponentType t;
            t.TypeIndex = TypeManager.GetTypeIndex<T>();
            t.AccessModeType = AccessMode.Subtractive;
            t.FixedArrayLength = -1;
            return t;
        }

        public ComponentType(Type type, AccessMode accessModeType = AccessMode.ReadWrite)
        {
            TypeIndex = TypeManager.GetTypeIndex(type);
            this.AccessModeType = accessModeType;
            FixedArrayLength = -1;
        }

        public static ComponentType FixedArray(Type type, int numElements)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (numElements < 0)
                throw new ArgumentException("FixedArray length must be 0 or larger");
#endif

            ComponentType t;
            t.TypeIndex = TypeManager.GetTypeIndex(type);
            t.AccessModeType = AccessMode.ReadWrite;
            t.FixedArrayLength = numElements;
            return t;
        }

        internal bool RequiresJobDependency
        {
            get
            {
                if (AccessModeType == AccessMode.Subtractive)
                    return false;
                var type = GetManagedType();
                //@TODO: This is wrong... Not right for fixed array, think about Entity array?
                return typeof(IComponentData).IsAssignableFrom(type);
            }
        }

        public Type GetManagedType()
        {
            return TypeManager.GetType(TypeIndex);
        }

        public static implicit operator ComponentType(Type type)
        {
            return new ComponentType(type, AccessMode.ReadWrite);
        }

        public static bool operator <(ComponentType lhs, ComponentType rhs)
        {
            if (lhs.TypeIndex == rhs.TypeIndex)
                return lhs.FixedArrayLength != rhs.FixedArrayLength ? lhs.FixedArrayLength < rhs.FixedArrayLength : lhs.AccessModeType < rhs.AccessModeType;

            return lhs.TypeIndex < rhs.TypeIndex;

        }
        public static bool operator >(ComponentType lhs, ComponentType rhs)
        {
            return rhs < lhs;
        }

        public static bool operator ==(ComponentType lhs, ComponentType rhs)
        {
            return lhs.TypeIndex == rhs.TypeIndex && lhs.FixedArrayLength == rhs.FixedArrayLength && lhs.AccessModeType == rhs.AccessModeType;
        }

        public static bool operator !=(ComponentType lhs, ComponentType rhs)
        {
            return lhs.TypeIndex != rhs.TypeIndex || lhs.FixedArrayLength != rhs.FixedArrayLength && lhs.AccessModeType == rhs.AccessModeType;
        }

        internal static unsafe bool CompareArray(ComponentType* type1, int typeCount1, ComponentType* type2, int typeCount2)
        {
            if (typeCount1 != typeCount2)
                return false;
            for (var i = 0; i < typeCount1; ++i)
            {
                if (type1[i] != type2[i])
                    return false;
            }
            return true;
        }

        public bool IsFixedArray => FixedArrayLength != -1;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        public override string ToString()
        {
            return IsFixedArray ? $"FixedArray(typeof({GetManagedType()}, {FixedArrayLength}))" : GetManagedType().ToString();
        }
#endif

        public override bool Equals(object obj)
        {
            return obj is ComponentType && (ComponentType) obj == this;
        }

        public override int GetHashCode()
        {
            return (TypeIndex * 5813) ^ FixedArrayLength;
        }
    }
}
