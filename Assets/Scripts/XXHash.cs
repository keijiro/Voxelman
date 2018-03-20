/// xxHash algorithm
public struct XXHash
{
    #region Private Members

    const uint PRIME32_1 = 2654435761U;
    const uint PRIME32_2 = 2246822519U;
    const uint PRIME32_3 = 3266489917U;
    const uint PRIME32_4 = 668265263U;
    const uint PRIME32_5 = 374761393U;

    static uint rotl32(uint x, int r)
    {
        return (x << r) | (x >> 32 - r);
    }

    #endregion

    #region Static Functions

    public static uint GetHash(uint data, uint seed)
    {
        uint h32 = seed + PRIME32_5;
        h32 += 4U;
        h32 += data * PRIME32_3;
        h32 = rotl32(h32, 17) * PRIME32_4;
        h32 ^= h32 >> 15;
        h32 *= PRIME32_2;
        h32 ^= h32 >> 13;
        h32 *= PRIME32_3;
        h32 ^= h32 >> 16;
        return h32;
    }

    #endregion

    #region Struct Implementation

    static uint _counter;

    public uint seed;

    public static XXHash RandomHash {
        get {
            return new XXHash(XXHash.GetHash(0xcafe, _counter++));
        }
    }

    public XXHash(uint seed)
    {
        this.seed = seed;
    }

    public uint GetHash(uint data)
    {
        return GetHash(data, seed);
    }

    public int Range(int max, uint data)
    {
        return (int)GetHash(data) % max;
    }

    public int Range(int min, int max, uint data)
    {
        return (int)GetHash(data) % (max - min) + min;
    }

    public float Value01(uint data)
    {
        return (GetHash(data) & 0x7fffffffu) / (float)0x7fffffff;
    }

    public float Range(float min, float max, uint data)
    {
        return Value01(data) * (max - min) + min;
    }

    #endregion
}
