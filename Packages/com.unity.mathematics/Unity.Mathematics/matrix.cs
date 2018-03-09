namespace Unity.Mathematics
{
    public partial struct float4x4
    {
        public float4 m0;
        public float4 m1;
        public float4 m2;
        public float4 m3;

        public float4x4(float4 m0, float4 m1, float4 m2, float4 m3)
        {
            this.m0 = m0;
            this.m1 = m1;
            this.m2 = m2;
            this.m3 = m3;
        }

        public float4x4(float m00, float m01, float m02, float m03,
            float m10, float m11, float m12, float m13,
            float m20, float m21, float m22, float m23,
            float m30, float m31, float m32, float m33)
        {
            this.m0 = new float4(m00, m01, m02, m03);
            this.m1 = new float4(m10, m11, m12, m13);
            this.m2 = new float4(m20, m21, m22, m23);
            this.m3 = new float4(m30, m31, m32, m33);
        }

        public static float4x4 operator *(float4x4 mat, float s)
        {
            return new float4x4(mat.m0 * s, mat.m1 * s, mat.m2 * s, mat.m3 * s);
        }

        public static float4x4 identity => new float4x4
        {
            m0 = new float4(1.0f, 0.0f, 0.0f, 0.0f),
            m1 = new float4(0.0f, 1.0f, 0.0f, 0.0f),
            m2 = new float4(0.0f, 0.0f, 1.0f, 0.0f),
            m3 = new float4(0.0f, 0.0f, 0.0f, 1.0f)
        };
    }

    public partial struct float2x2
    {
        public float2 m0;
        public float2 m1;

        public float2x2(float2 m0, float2 m1)
        {
            this.m0 = m0;
            this.m1 = m1;
        }

        public float2x2(float m00, float m01,
            float m10, float m11)
        {
            this.m0 = new float2(m00, m01);
            this.m1 = new float2(m10, m11);
        }

        public static float2x2 operator *(float2x2 mat, float s)
        {
            return new float2x2(mat.m0 * s, mat.m1 * s);
        }
    }

    public partial struct float3x3
    {
        public float3 m0;
        public float3 m1;
        public float3 m2;

        public float3x3(float3 m0, float3 m1, float3 m2)
        {
            this.m0 = m0;
            this.m1 = m1;
            this.m2 = m2;
        }

        public float3x3(float m00, float m01, float m02,
            float m10, float m11, float m12,
            float m20, float m21, float m22)
        {
            this.m0 = new float3(m00, m01, m02);
            this.m1 = new float3(m10, m11, m12);
            this.m2 = new float3(m20, m21, m22);
        }

        public static float3x3 operator *(float3x3 mat, float s)
        {
            return new float3x3(mat.m0 * s, mat.m1 * s, mat.m2 * s);
        }
    }

    partial class math
    {
        public static float4 mul(float4x4 x, float4 v)
        {
            return mad(x.m0, v.x, x.m1 * v.y) + mad(x.m2, v.z, x.m3 * v.w);
        }

        public static float4x4 mul(float4x4 a, float4x4 b)
        {
            return new float4x4(mul(a, b.m0), mul(a, b.m1), mul(a, b.m2), mul(a, b.m3));
        }

        public static float2 mul(float2x2 x, float2 v)
        {
            return mad(x.m0, v.x, x.m1 * v.y);
        }

        public static float2x2 mul(float2x2 a, float2x2 b)
        {
            return new float2x2(mul(a, b.m0), mul(a, b.m1));
        }

        public static float3 mul(float3x3 x, float3 v)
        {
            return mad(x.m2, v.z, mad(x.m0, v.x, x.m1 * v.y));
        }

        public static float3x3 mul(float3x3 a, float3x3 b)
        {
            return new float3x3(mul(a, b.m0), mul(a, b.m1), mul(a, b.m2));
        }

        public static float3x3 orthogonalize(float3x3 i)
        {
            float3x3 o;

            float3 u = i.m0;
            float3 v = i.m1 - i.m0 * math.dot(i.m1, i.m0);

            float lenU = math.length(u);
            float lenV = math.length(v);

            bool c = lenU > epsilon_normal && lenV > epsilon_normal;

            o.m0 = math.select(new float3(1, 0, 0), u / lenU, c);
            o.m1 = math.select(new float3(0, 1, 0), v / lenV, c);
            o.m2 = math.cross(o.m0, o.m1);

            return o;
        }

        public static float2x2 transpose(float2x2 i) { return new float2x2(i.m0.x, i.m1.x, i.m0.y, i.m1.y); }
        public static float3x3 transpose(float3x3 i) { return new float3x3(i.m0.x, i.m1.x, i.m2.x, i.m0.y, i.m1.y, i.m2.y, i.m0.z, i.m1.z, i.m2.z); }
        public static float4x4 transpose(float4x4 i) { return new float4x4(i.m0.x, i.m1.x, i.m2.x, i.m3.x, i.m0.y, i.m1.y, i.m2.y, i.m3.y, i.m0.z, i.m1.z, i.m2.z, i.m3.z, i.m0.w, i.m1.w, i.m2.w, i.m3.w); }
        
        public static float4x4 scale(float3 vector)
        {
            float4x4 matrix4x4 = new float4x4();
            matrix4x4.m0 = new float4(vector.x,0.0f,0.0f,0.0f);
            matrix4x4.m1 = new float4(0.0f,vector.y,0.0f,0.0f);
            matrix4x4.m2 = new float4(0.0f,0.0f,vector.z,0.0f);
            matrix4x4.m3 = new float4(0.0f, 0.0f, 0.0f, 1.0f);
            return matrix4x4;
        }

        public static float4x4 translate(float3 vector)
        {
            float4x4 matrix4x4 = new float4x4();
            matrix4x4.m0 = new float4(1.0f, 0.0f, 0.0f, 0.0f);
            matrix4x4.m1 = new float4(0.0f, 1.0f, 0.0f, 0.0f);
            matrix4x4.m2 = new float4(0.0f, 0.0f, 1.0f, 0.0f);
            matrix4x4.m3 = new float4(vector.x, vector.y, vector.z, 1.0f);
            return matrix4x4;
        }        

        const float epsilon = 0.000001F;

        public static float3x3 identity3
        {
            get { return new float3x3(new float3(1, 0, 0), new float3(0, 1, 0), new float3(0, 0, 1)); }
        }
        public static float4x4 identity4
        {
            get { return new float4x4(new float4(1, 0, 0, 0), new float4(0, 1, 0, 0), new float4(0, 0, 1, 0), new float4(0, 0, 0, 1)); }
        }

        public static float4x4 lookRotationToMatrix(float3 position, float3 forward, float3 up)
        {
            float3x3 rot = lookRotationToMatrix(forward, up);

            float4x4 matrix;
            matrix.m0 = new float4(rot.m0, 0.0F);
            matrix.m1 = new float4(rot.m1, 0.0F);
            matrix.m2 = new float4(rot.m2, 0.0F);
            matrix.m3 = new float4(position, 1.0F);

            return matrix;
        }

        public static float3x3 lookRotationToMatrix(float3 forward, float3 up)
        {
            float3 z = forward;
            // compute u0
            float mag = math.length(z);
            if (mag < epsilon)
                return identity3;
            z /= mag;

            float3 x = math.cross(up, z);
            mag = math.length(x);
            if (mag < epsilon)
                return identity3;
            x /= mag;

            float3 y = math.cross(z, x);
            float yLength = math.length(y);
            if (yLength < 0.9F || yLength > 1.1F)
                return identity3;

            return new float3x3(x, y, z);
        }

        /* @TODO:
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static float determinant(float2x2 x) { throw new System.NotImplementedException(); }
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static float determinant(float3x3 x) { throw new System.NotImplementedException(); }
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static float determinant(float4x4 x) { throw new System.NotImplementedException(); }
        */
    }
}
