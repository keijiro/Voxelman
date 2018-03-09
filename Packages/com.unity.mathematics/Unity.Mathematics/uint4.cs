using System.Runtime.CompilerServices;
using System.Diagnostics;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [DebuggerTypeProxy(typeof(uint4.DebuggerProxy))]
    [System.Serializable]
    public partial struct uint4
    {
        internal sealed class DebuggerProxy
        {
            public uint x;
            public uint y;
            public uint z;
            public uint w;

            public DebuggerProxy(uint4 vec)
            {
                x = vec.x;
                y = vec.y;
                z = vec.z;
                w = vec.z;
            }
        }

        public uint x;
        public uint y;
        public uint z;
        public uint w;

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint4(uint x, uint y, uint z, uint w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint4(uint val) { x = y = z = w = val; }

        // FIXME
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint4(uint3 xyz, uint w) { this.x = xyz.x; this.y = xyz.y; this.z = xyz.z; this.w = w; }
        
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint4(int4 val) { x = (uint)val.x; y = (uint)val.y; z = (uint)val.z; w = (uint)val.w; }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public uint4(float4 val) { x = (uint)val.x; y = (uint)val.y; z = (uint)val.z; w = (uint)val.w; }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static implicit operator uint4(uint d) { return new uint4(d); }
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static explicit operator uint4(float4 d) { return new uint4((uint)d.x, (uint)d.y, (uint)d.z, (uint)d.w); }

        public override string ToString()
        {
            return string.Format("uint4({0}, {1}, {2}, {3})", x, y, z, w);
        }
    }
}

