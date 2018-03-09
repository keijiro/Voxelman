using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Unity.Mathematics
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct IntFloatUnion
    {
        [FieldOffset(0)]
        public int intValue;
        [FieldOffset(0)]
        public float floatValue;

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public IntFloatUnion(float value)
        {
            intValue = 0;
            floatValue = (float)value;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public IntFloatUnion(int value)
        {
            floatValue = 0;
            intValue = (int)value;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static int ToInt(float value)
        {
            return new IntFloatUnion(value).intValue;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static float ToFloat(int value)
        {
            return new IntFloatUnion(value).floatValue;
        }
    }
}

