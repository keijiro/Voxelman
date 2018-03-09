using System;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;

using Component = UnityEngine.Component;

namespace Unity.Entities
{
    static unsafe class TypeManager
    {
        struct StaticTypeLookup<T>
        {
            public static int typeIndex;
        }

        internal enum TypeCategory
        {
            ComponentData,
            ISharedComponentData,
            OtherValueType,
            EntityData,
            Class
        }

        internal struct ComponentType
        {
            public ComponentType(Type type, int size, TypeCategory category, FastEquality.Layout[] layout)
            {
                Type = type;
                SizeInChunk = size;
                Category = category;
                FastEqualityLayout = layout;
            }

            public readonly Type                  Type;
            public readonly int                   SizeInChunk;
            public readonly FastEquality.Layout[] FastEqualityLayout;
            public readonly TypeCategory          Category;
        }

        static ComponentType[]    s_Types;
        static volatile int       s_Count;
        static SpinLock           s_CreateTypeLock;

        public const int MaximumTypesCount = 1024 * 10;

        public static int ObjectOffset;

        // TODO: this creates a dependency on UnityEngine, but makes splitting code in separate assemblies easier. We need to remove it during the biggere refactor.
        internal static readonly Type UnityEngineComponentType = typeof(Component);

        struct ObjectOffsetType
        {
            void* v0;
            void* v1;
        }

        public static void Initialize()
        {
            if (s_Types != null)
                return;

            ObjectOffset = UnsafeUtility.SizeOf<ObjectOffsetType>();
            s_CreateTypeLock = new SpinLock();
            s_Types = new ComponentType[MaximumTypesCount];
            s_Count = 0;

            s_Types[s_Count++] = new ComponentType(null, 0, TypeCategory.ComponentData, null);
            // This must always be first so that Entity is always index 0 in the archetype
            s_Types[s_Count++] = new ComponentType(typeof(Entity), sizeof(Entity), TypeCategory.EntityData, FastEquality.CreateLayout(typeof(Entity)));
        }


        public static int GetTypeIndex<T>()
        {
            var typeIndex = StaticTypeLookup<T>.typeIndex;
            if (typeIndex != 0)
                return typeIndex;

            typeIndex = GetTypeIndex (typeof(T));
            StaticTypeLookup<T>.typeIndex = typeIndex;
            return typeIndex;
        }

        public static int GetTypeIndex(Type type)
        {
            var index = FindTypeIndex(type, s_Count);
            return index != -1 ? index : CreateTypeIndexThreadSafe(type);
        }

        static int FindTypeIndex(Type type, int count)
        {
            for (var i = 0; i != count; i++)
            {
                var c = s_Types[i];
                if (c.Type == type)
                    return i;
            }
            return -1;
        }

        static int CreateTypeIndexThreadSafe(Type type)
        {
            var lockTaken = false;
            try
            {
                s_CreateTypeLock.Enter(ref lockTaken);

                // After taking the lock, make sure the type hasn't been created
                // after doing the non-atomic FindTypeIndex
                var index = FindTypeIndex(type, s_Count);
                if (index != -1)
                    return index;

                var compoentType = BuildComponentType(type);

                index = s_Count++;
                s_Types[index] = compoentType;

                return index;
            }
            finally
            {
                if (lockTaken)
                    s_CreateTypeLock.Exit(true);
            }
        }

        static ComponentType BuildComponentType(Type type)
        {
            var componentSize = 0;
            TypeCategory category;
            FastEquality.Layout[] fastEqualityLayout = null;
            if (typeof(IComponentData).IsAssignableFrom(type))
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if (type.IsClass)
                    throw new ArgumentException($"{type} is an IComponentData, and thus must be a struct.");
                if (!UnsafeUtility.IsBlittable(type))
                    throw new ArgumentException($"{type} is an IComponentData, and thus must be blittable (No managed object is allowed on the struct).");
#endif

                category = TypeCategory.ComponentData;
                componentSize = UnsafeUtility.SizeOf(type);
                fastEqualityLayout = FastEquality.CreateLayout(type);
            }
            else if (typeof(ISharedComponentData).IsAssignableFrom(type))
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if (type.IsClass)
                    throw new ArgumentException($"{type} is an ISharedComponentData, and thus must be a struct.");
#endif

                category = TypeCategory.ISharedComponentData;
                fastEqualityLayout = FastEquality.CreateLayout(type);
            }
            else if (type.IsValueType)
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if (!UnsafeUtility.IsBlittable(type))
                    throw new ArgumentException($"{type} is used for FixedArrays, and thus must be blittable.");
#endif
                category = TypeCategory.OtherValueType;
                componentSize = UnsafeUtility.SizeOf(type);
            }
            else if (type.IsClass)
            {
                category = TypeCategory.Class;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if (type == typeof(GameObjectEntity))
                    throw new ArgumentException("GameObjectEntity can not be used from EntityManager. The component is ignored when creating entities for a GameObject.");
#endif
            }
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            else
            {
                throw new ArgumentException($"'{type}' is not a valid component");
            }
#else
            else
            {
                category = TypeCategory.OtherValueType;
            }
#endif

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (typeof(IComponentData).IsAssignableFrom(type) && typeof(ISharedComponentData).IsAssignableFrom(type))
                throw new ArgumentException($"Component {type} can not be both IComponentData & ISharedComponentData");
#endif
            return new ComponentType(type, componentSize, category, fastEqualityLayout);
        }

        public static bool IsValidComponentTypeForArchetype(int typeIndex, bool isArray)
        {
            if (s_Types[typeIndex].Category == TypeCategory.OtherValueType)
                return isArray;
            return !isArray;
        }

        public static ComponentType GetComponentType(int typeIndex)
        {
            return s_Types[typeIndex];
        }

        public static ComponentType GetComponentType<T>()
        {
            return s_Types[GetTypeIndex<T>()];
        }

        public static Type GetType(int typeIndex)
        {
            return s_Types[typeIndex].Type;
        }

        public static int GetTypeCount()
        {
            return s_Count;
        }
    }
}

