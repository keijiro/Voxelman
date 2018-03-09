using System.Runtime.CompilerServices;
using System.Diagnostics;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [DebuggerTypeProxy(typeof(int2.DebuggerProxy))]
    [System.Serializable]
    public partial struct int2
    {
        internal sealed class DebuggerProxy
        {
            public int x;
            public int y;

            public DebuggerProxy(int2 vec)
            {
                x = vec.x;
                y = vec.y;
            }
        }

        public int x;
        public int y;

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public int2(int val) { x = y = val; }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public int2(float2 val)
        {
            x = (int)val.x;
            y = (int)val.y;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static implicit operator int2(int d) { return new int2(d); }
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static explicit operator int2(float2 d) { return new int2((int)d.x, (int)d.y); }

        public override string ToString()
        {
            return string.Format("int2({0}, {1})", x, y);
        }
    }
}

