using System;
using System.Reflection;
using Boo.Lang;
using Unity.Collections.LowLevel.Unsafe;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Unity.Entities.Tests")]

namespace Unity.Entities
{
    static internal class FastEquality
    {
        internal struct Layout
        {
            public int  offset;
            public int  count;
            public bool Aligned4;

            public override string ToString()
            {
                return $"offset: {offset} count: {count} Aligned4: {Aligned4}";
            }
        }
        
        
        internal static Layout[] CreateLayout(Type type)
        {
            int begin = 0;
            int end = 0;

            var layouts = new List<Layout>();

            CreateLayoutRecurse(type, 0, layouts, ref begin, ref end);
            
            if (begin != end)
                layouts.Add(new Layout {offset = begin, count = end - begin, Aligned4 = false});

            var layoutsArray = layouts.ToArray();

            for (int i = 0; i != layoutsArray.Length; i++)
            {
                if (layoutsArray[i].count % 4 == 0 && layoutsArray[i].offset % 4 == 0)
                {
                   layoutsArray[i].count /= 4;
                   layoutsArray[i].Aligned4 = true;
                }
            }

            return layoutsArray;
        }

        unsafe struct PointerSize
        {
            void* pter;
        }

        static void CreateLayoutRecurse(Type type, int baseOffset, List<Layout> layouts, ref int begin, ref int end)
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var field in fields)
            {
                int offset = baseOffset + UnsafeUtility.GetFieldOffset(field);

                if (field.FieldType.IsPrimitive || field.FieldType.IsPointer || field.FieldType.IsClass)
                {
                    int sizeOf = -1;
                    if (field.FieldType.IsPointer || field.FieldType.IsClass)
                        sizeOf = UnsafeUtility.SizeOf<PointerSize>();
                    else
                        sizeOf = UnsafeUtility.SizeOf(field.FieldType);
                        
                    if (end != offset)
                    {
                        layouts.Add(new Layout {offset = begin, count = end - begin, Aligned4 = false});
                        begin = offset;
                        end = offset + sizeOf;
                    }
                    else
                    {
                        end += sizeOf;
                    }
                }
                else
                {
                    CreateLayoutRecurse(field.FieldType, offset, layouts, ref begin, ref end);
                }
            }
        }

        //@TODO: Encode type in hashcode...
        
        const int FNV_32_PRIME = 0x01000193;

        unsafe public static int GetHashCode<T>(T lhs, Layout[] layout) where T : struct
        {
            return GetHashCode(UnsafeUtility.AddressOf(ref lhs), layout);
        }

        unsafe public static int GetHashCode<T>(ref T lhs, Layout[] layout) where T : struct
        {
            return GetHashCode(UnsafeUtility.AddressOf(ref lhs), layout);
        }

        unsafe public static int GetHashCode(void* dataPtr, Layout[] layout)
        {
            byte* data = (byte*)dataPtr;
            uint hash = 0;
            
            for (int k = 0; k != layout.Length; k++)
            {
                if (layout[k].Aligned4)
                {
                    uint* dataInt = (uint*)(data + layout[k].offset);
                    int count = layout[k].count;
                    for (int i = 0; i != count; i++)
                    {
                        hash *= FNV_32_PRIME;
                        hash ^= dataInt[i];
                    }
                }
                else
                {
                    byte* dataByte = data + layout[k].offset;
                    int count = layout[k].count;
                    for (int i = 0; i != count; i++)
                    {
                        hash *= FNV_32_PRIME;
                        hash ^= (uint)dataByte[i];
                    }
                }
            }

            return (int)hash;
        }
        
        unsafe public static bool Equals<T>(T lhs, T rhs, Layout[] layout) where T : struct
        {
            return Equals(UnsafeUtility.AddressOf(ref lhs), UnsafeUtility.AddressOf(ref rhs), layout);
        }

        unsafe public static bool Equals<T>(ref T lhs, ref T rhs, Layout[] layout) where T : struct
        {
            return Equals(UnsafeUtility.AddressOf(ref lhs), UnsafeUtility.AddressOf(ref rhs), layout);
        }

        unsafe public static bool Equals(void* lhsPtr, void* rhsPtr, Layout[] layout)
        {
            byte* lhs = (byte*)lhsPtr;
            byte* rhs = (byte*)rhsPtr;
            
            bool same = true;
            
            for (int k = 0; k != layout.Length; k++)
            {
                if (layout[k].Aligned4)
                {
                    int offset = layout[k].offset;
                    uint* lhsInt = (uint*)(lhs + offset);
                    uint* rhsInt = (uint*)(rhs + offset);
                    int count = layout[k].count;
                    for (int i = 0; i != count; i++)
                        same &= lhsInt[i] == rhsInt[i];
                }
                else
                {
                    int offset = layout[k].offset;
                    byte* lhsByte = lhs + offset;
                    byte* rhsByte = rhs + offset;
                    int count = layout[k].count;
                    for (int i = 0; i != count; i++)
                        same &= lhsByte[i] == rhsByte[i];
                }
            }

            return same;
        }
    }
}
