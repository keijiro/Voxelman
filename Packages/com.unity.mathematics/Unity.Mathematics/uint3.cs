using System.Runtime.CompilerServices;
using System.Diagnostics;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [DebuggerTypeProxy(typeof(uint3.DebuggerProxy))]
    [System.Serializable]
    public partial struct uint3
    {
        internal sealed class DebuggerProxy
        {
            public uint x;
            public uint y;
            public uint z;

            public DebuggerProxy(uint3 vec)
            {
                x = vec.x;
                y = vec.y;
                z = vec.z;
            }
        }

        public uint x;
        public uint y;
        public uint z;

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint3(uint val) { x = y = z = val; }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint3(uint x, uint y, uint z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint3(uint2 xy, uint z)
        {
            this.x = xy.x;
            this.y = xy.y;
            this.z = z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint3(float3 val)
        {
            x = (uint)val.x;
            y = (uint)val.y;
            z = (uint)val.z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static implicit operator uint3(uint d) { return new uint3(d); }
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static explicit operator uint3(float3 d) { return new uint3(d); }

        public override string ToString()
        {
            return string.Format("uint3({0}, {1}, {2})", x, y, z);
        }

    }
}

