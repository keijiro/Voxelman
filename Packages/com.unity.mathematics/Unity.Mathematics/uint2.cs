using System.Runtime.CompilerServices;
using System.Diagnostics;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [DebuggerTypeProxy(typeof(uint2.DebuggerProxy))]
    [System.Serializable]
    public partial struct uint2
    {
        internal sealed class DebuggerProxy
        {
            public uint x;
            public uint y;

            public DebuggerProxy(uint2 vec)
            {
                x = vec.x;
                y = vec.y;
            }
        }

        public uint x;
        public uint y;

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint2(uint val) { x = y = val; }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint2(uint x, uint y)
        {
            this.x = x;
            this.y = y;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint2(int2 val)
        {
            x = (uint)val.x;
            y = (uint)val.y;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint2(float2 val)
        {
            x = (uint)val.x;
            y = (uint)val.y;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static implicit operator uint2(uint d) { return new uint2(d); }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static explicit operator uint2(float2 d) { return new uint2(d); }

        public override string ToString()
        {
            return string.Format("uint2({0}, {1})", x, y);
        }
    }
}

