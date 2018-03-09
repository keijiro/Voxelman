using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;

#pragma warning disable 0660, 0661
namespace Unity.Mathematics
{
    [DebuggerTypeProxy(typeof(float3.DebuggerProxy))]
    [System.Serializable]
    public partial struct float3 : IFormattable
    {
        internal sealed class DebuggerProxy
        {
            public float x;
            public float y;
            public float z;

            public DebuggerProxy(float3 vec)
            {
                x = vec.x;
                y = vec.y;
                z = vec.z;
            }
        }

        public float x;
        public float y;
        public float z;

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public float3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public float3(float val) { x = y = z = val; }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public float3(float2 xy, float z)
        {
            this.x = xy.x;
            this.y = xy.y;
            this.z = z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public float3(int val) { x = y = z = val; }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public float3(int3 val)
        {
            x = val.x;
            y = val.y;
            z = val.z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static implicit operator float3(float d) { return new float3(d); }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static implicit operator float3(int3 d) { return new float3(d.x, d.y, d.z); }

        public override string ToString()
        {
            return string.Format("float3({0}f, {1}f, {2}f)", x, y, z);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("float3({0}f, {1}f, {2}f)", x.ToString(format, formatProvider), y.ToString(format, formatProvider), z.ToString(format, formatProvider));
        }
    }
}

