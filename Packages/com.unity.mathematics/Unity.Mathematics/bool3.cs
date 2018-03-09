using System.Runtime.CompilerServices;
using System.Diagnostics;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [DebuggerTypeProxy(typeof(bool3.DebuggerProxy))]
    public partial struct bool3
    {
        internal sealed class DebuggerProxy
        {
            public bool x;
            public bool y;
            public bool z;

            public DebuggerProxy(bool3 vec)
            {
                x = vec.x;
                y = vec.y;
                z = vec.z;
            }
        }

        public bool1 x;
        public bool1 y;
        public bool1 z;

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public bool3(bool val) { x = y = z = val; }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public bool3(bool x, bool y, bool z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public bool3(bool2 xy, bool z)
        {
            this.x = xy.x;
            this.y = xy.y;
            this.z = z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static implicit operator bool3(bool d)
        {
            return new bool3(d);
        }

        public override string ToString()
        {
            return string.Format("bool3({0}, {1}, {2})", x, y, z);
        }

    }
}

