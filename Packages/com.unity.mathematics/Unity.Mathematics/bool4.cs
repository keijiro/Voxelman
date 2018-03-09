using System.Runtime.CompilerServices;
using System.Diagnostics;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [DebuggerTypeProxy(typeof(bool4.DebuggerProxy))]
    public partial struct bool4
    {
        internal sealed class DebuggerProxy
        {
            public bool x;
            public bool y;
            public bool z;
            public bool w;

            public DebuggerProxy(bool4 vec)
            {
                x = vec.x;
                y = vec.y;
                z = vec.z;
                w = vec.z;
            }
        }

        public bool1 x;
        public bool1 y;
        public bool1 z;
        public bool1 w;

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public bool4(bool x, bool y, bool z, bool w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public bool4(bool val)
        {
            x = y = z = w = val;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public bool4(bool3 xyz, bool w)
        {
            x = xyz.x; y = xyz.y; z = xyz.z; this.w = w;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static implicit operator bool4(bool d)
        {
            return new bool4(d);
        }

        public override string ToString()
        {
            return string.Format("bool4({0}, {1}, {2}, {3})", x, y, z, w);
        }
    }
}

