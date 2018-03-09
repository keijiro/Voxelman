using System.Runtime.InteropServices;
using NUnit.Framework;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.ECS.Tests
{
    public class FastEqualityTests
    {
        [StructLayout(LayoutKind.Sequential)]
        struct Simple
        {
            int a;
            int b;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SimpleEmbedded
        {
            float4 a;
            int b;
        }
        
        [StructLayout(LayoutKind.Sequential)]

        struct BytePadding
        {
            byte a;
            byte b;
            float c;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        struct AlignSplit
        {
            float3 a;
            double b;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        struct EndPadding
        {
            double a;
            float b;

            public EndPadding(double a, float b)
            {
                this.a = a;
                this.b = b;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        unsafe struct FloatPointer
        {
            float* a;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        struct ClassInStruct
        {
            string blah;
        }


        int PtrAligned4Count = UnsafeUtility.SizeOf<FloatPointer>() / 4;
        
        [Test]
        public void SimpleLayout()
        {
            var res = FastEquality.CreateLayout(typeof(Simple));
            Assert.AreEqual(1, res.Length);
            Assert.AreEqual(new FastEquality.Layout {offset = 0, count = 2, Aligned4 = true }, res[0]);
            
            
        }

        [Test]
        public void PtrLayout()
        {
            var layout = FastEquality.CreateLayout(typeof(FloatPointer));
            Assert.AreEqual(1, layout.Length);
            Assert.AreEqual(new FastEquality.Layout {offset = 0, count = PtrAligned4Count, Aligned4 = true }, layout[0]);
        }
        
        [Test]
        public void ClassLayout()
        {
            var layout = FastEquality.CreateLayout(typeof(ClassInStruct));
            Assert.AreEqual(1, layout.Length);
            Assert.AreEqual(new FastEquality.Layout {offset = 0, count = PtrAligned4Count, Aligned4 = true }, layout[0]);
        }
        
        [Test]
        public void SimpleEmbeddedLayout()
        {
            var layout = FastEquality.CreateLayout(typeof(SimpleEmbedded));
            Assert.AreEqual(1, layout.Length);
            Assert.AreEqual(new FastEquality.Layout {offset = 0, count = 5, Aligned4 = true }, layout[0]);
        }
        
        [Test]
        public void EndPaddingLayout()
        {
            var layout = FastEquality.CreateLayout(typeof(EndPadding));
            Assert.AreEqual(1, layout.Length);
            Assert.AreEqual(new FastEquality.Layout {offset = 0, count = 3, Aligned4 = true }, layout[0]);
        }
        
        [Test]
        public void AlignSplitLayout()
        {
            var layout = FastEquality.CreateLayout(typeof(AlignSplit));
            Assert.AreEqual(2, layout.Length);
            
            Assert.AreEqual(new FastEquality.Layout {offset = 0, count = 3, Aligned4 = true }, layout[0]);
            Assert.AreEqual(new FastEquality.Layout {offset = 16, count = 2, Aligned4 = true }, layout[1]);
        }
        
        [Test]
        public void BytePaddding()
        {
            var layout = FastEquality.CreateLayout(typeof(BytePadding));
            Assert.AreEqual(2, layout.Length);
            
            Assert.AreEqual(new FastEquality.Layout {offset = 0, count = 2, Aligned4 = false }, layout[0]);
            Assert.AreEqual(new FastEquality.Layout {offset = 4, count = 1, Aligned4 = true }, layout[1]);
        }
        
        [Test]
        public void EqualsInt4()
        {
            var layout = FastEquality.CreateLayout(typeof(int4));
                        
            Assert.IsTrue(FastEquality.Equals(new int4(1, 2, 3, 4), new int4(1, 2, 3, 4), layout));
            Assert.IsFalse(FastEquality.Equals(new int4(1, 2, 3, 4), new int4(1, 2, 3, 5), layout));
            Assert.IsFalse(FastEquality.Equals(new int4(1, 2, 3, 4), new int4(0, 2, 3, 4), layout));
            Assert.IsFalse(FastEquality.Equals(new int4(1, 2, 3, 4), new int4(5, 6, 7, 8), layout));
        }
        
        [Test]
        public void EqualsPadding()
        {
            var layout = FastEquality.CreateLayout(typeof(EndPadding));
            
            Assert.IsTrue(FastEquality.Equals(new EndPadding(1, 2), new EndPadding(1, 2), layout));
            Assert.IsFalse(FastEquality.Equals(new EndPadding(1, 2), new EndPadding(1, 3), layout));
            Assert.IsFalse(FastEquality.Equals(new EndPadding(1, 2), new EndPadding(4, 2), layout));
        }
        
        [Test]
        unsafe public void GetHashCodeInt4()
        {
            var layout = FastEquality.CreateLayout(typeof(int4));
            Assert.AreEqual(-270419516, FastEquality.GetHashCode(new int4(1, 2, 3, 4), layout));
            Assert.AreEqual(-270419517, FastEquality.GetHashCode(new int4(1, 2, 3, 3), layout));
            Assert.AreEqual(1, FastEquality.GetHashCode(new int4(0, 0, 0, 1), layout));
            Assert.AreEqual(16777619, FastEquality.GetHashCode(new int4(0, 0, 1, 0), layout));
            Assert.AreEqual(0, FastEquality.GetHashCode(new int4(0, 0, 0, 0), layout));

            // Note, builtin .GetHashCode() returns different values even for all zeros...
        }
    }
}
