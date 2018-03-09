using System;

namespace Unity.Mathematics
{
    [Serializable]
    public partial struct quaternion
    {
        public float4 value;

        public quaternion(float x, float y, float z, float w) { value.x = x; value.y = y; value.z = z; value.w = w; }
        public quaternion(float4 value)                       { this.value = value; }

        public static quaternion identity { get { return new quaternion(0.0F, 0.0F, 0.0F, 1.0F); } }

    }

    public static partial class math
    {
        public static quaternion normalize(quaternion q)
        {
            float4 value = q.value;
            float len = math.dot(value, value);

            // note we use float4 comparison here because this gives us -1 / 0 which is necessary for select.
            //return select(quatIdentity(), q*rsqrt(len), len > float4(epsilon_normal()));
            value = math.select(quaternion.identity.value, value * math.rsqrt(len), len > math.epsilon_normal);

            return new quaternion(value);
        }

        public static quaternion mul(quaternion lhs, quaternion rhs)
        {
            return new quaternion(
                lhs.value.w* rhs.value.x + lhs.value.x* rhs.value.w + lhs.value.y* rhs.value.z - lhs.value.z* rhs.value.y,
                lhs.value.w* rhs.value.y + lhs.value.y* rhs.value.w + lhs.value.z* rhs.value.x - lhs.value.x* rhs.value.z,
                lhs.value.w* rhs.value.z + lhs.value.z* rhs.value.w + lhs.value.x* rhs.value.y - lhs.value.y* rhs.value.x,
                lhs.value.w* rhs.value.w - lhs.value.x* rhs.value.x - lhs.value.y* rhs.value.y - lhs.value.z* rhs.value.z);
        }

        public static float3 mul(quaternion rotation, float3 position)
        {
            float x = rotation.value.x * 2F;
            float y = rotation.value.y * 2F;
            float z = rotation.value.z * 2F;
            float xx = rotation.value.x * x;
            float yy = rotation.value.y * y;
            float zz = rotation.value.z * z;
            float xy = rotation.value.x * y;
            float xz = rotation.value.x * z;
            float yz = rotation.value.y * z;
            float wx = rotation.value.w * x;
            float wy = rotation.value.w * y;
            float wz = rotation.value.w * z;

            float3 res;
            res.x = (1F - (yy + zz)) * position.x + (xy - wz) * position.y + (xz + wy) * position.z;
            res.y = (xy + wz) * position.x + (1F - (xx + zz)) * position.y + (yz - wx) * position.z;
            res.z = (xz - wy) * position.x + (yz + wx) * position.y + (1F - (xx + yy)) * position.z;
            return res;
        }

        // get unit quaternion from rotation matrix
        // u, v, w must be ortho-normal.
        public static quaternion matrixToQuat(float3 u, float3 v, float3 w)
        {
            float4 q;
            if (u.x >= 0f)
            {
                float t = v.y + w.z;
                if (t >= 0f)
                    q = new float4(v.z - w.y, w.x - u.z, u.y - v.x, 1f + u.x + t);
                else
                    q = new float4(1f + u.x - t, u.y + v.x, w.x + u.z, v.z - w.y);
            }
            else
            {
                float t = v.y - w.z;
                if (t >= 0f)
                    q = new float4(u.y + v.x, 1f - u.x + t, v.z + w.y, w.x - u.z);
                else
                    q = new float4(w.x + u.z, v.z + w.y, 1f - u.x - t, u.y - v.x);
            }
            return normalize(new quaternion(q));
        }

        public static float3x3 quatToMatrix(quaternion q)
        {
            q = math.normalize(q);
            
            // Precalculate coordinate products
            float x = q.value.x * 2.0F;
            float y = q.value.y * 2.0F;
            float z = q.value.z * 2.0F;
            float xx = q.value.x * x;
            float yy = q.value.y * y;
            float zz = q.value.z * z;
            float xy = q.value.x * y;
            float xz = q.value.x * z;
            float yz = q.value.y * z;
            float wx = q.value.w * x;
            float wy = q.value.w * y;
            float wz = q.value.w * z;

            // Calculate 3x3 matrix from orthonormal basis
            var m = new float3x3
            {
                m0 = new float3(1.0f - (yy + zz), xy + wz, xz - wy),
                m1 = new float3(xy - wz, 1.0f - (xx + zz), yz + wx),
                m2 = new float3(xz + wy, yz - wx, 1.0f - (xx + yy))
            };
            return m;
        }

        public static float4x4 rottrans(quaternion q, float3 t)
        {
            var m3x3 = quatToMatrix(q);
            var m = new float4x4
            {
                m0 = new float4(m3x3.m0, 0.0f),
                m1 = new float4(m3x3.m1, 0.0f),
                m2 = new float4(m3x3.m2, 0.0f),
                m3 = new float4(t, 1.0f)
            };
            return m;
        }
        
        public static quaternion axisAngle(float3 axis, float angle)
        {
            float3 axisUnit  = math.normalize(axis);
            float sina = math.sin(0.5f * angle);
            float cosa = math.cos(0.5f * angle);
            return new quaternion { value = new float4( axisUnit.x * sina, axisUnit.y * sina, axisUnit.z * sina, cosa ) };
        }

        //@TODO: Seperated x, y, z
        public static quaternion euler(float3 eulerInDegrees)
        {
            throw new System.NotImplementedException();
        }

        //@TODO: Decide on saturate for t (old math lib did it...)

        public static quaternion slerp(quaternion lhs, quaternion rhs, float t)
        {
            throw new System.NotImplementedException();
        }

        public static quaternion lerp(quaternion lhs, quaternion rhs, float t)
        {
            throw new System.NotImplementedException();
            // var res = math.normalize(lhs.value + t * (math.chgsign(rhs.value, math.dot(lhs.value, rhs.value)) - rhs.value));
            // return new quaternion(res);
        }

        public static float3 forward(quaternion q)
        {
            return mul(q, new float3(0, 0, 1));
        }
        
        public static float3 up(quaternion q)
        {
            return mul(q, new float3(0, 1, 0));
        }

        public static quaternion lookRotationToQuaternion(float3 direction, float3 up)
        {
            var m = lookRotationToMatrix(direction, up);
            var q = matrixToQuat(m.m0,m.m1,m.m2);
            return q;
        }
    }
}
