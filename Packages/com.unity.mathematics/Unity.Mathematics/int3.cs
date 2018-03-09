using System.Runtime.CompilerServices;
using System.Diagnostics;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [DebuggerTypeProxy(typeof(int3.DebuggerProxy))]
    [System.Serializable]
    public partial struct int3
    {
        internal sealed class DebuggerProxy
        {
            public int x;
            public int y;
            public int z;

            public DebuggerProxy(int3 vec)
            {
                x = vec.x;
                y = vec.y;
                z = vec.z;
            }
        }

        public int x;
        public int y;
        public int z;

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public int3(int val) { x = y = z = val; }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public int3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public int3(int2 xy, int z)
        {
            this.x = xy.x;
            this.y = xy.y;
            this.z = z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public int3(float3 val)
        {
            x = (int)val.x;
            y = (int)val.y;
            z = (int)val.z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static implicit operator int3(int d) { return new int3(d); }
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static explicit operator int3(float3 d) { return new int3((int)d.x, (int)d.y, (int)d.z); }

        public override string ToString()
        {
            return string.Format("int3({0}, {1}, {2})", x, y, z);
        }

    }
}

