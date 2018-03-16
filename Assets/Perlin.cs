using Unity.Mathematics;

public static class Perlin
{
    static int perm(int x)
    {
        return (((x * 34) + 1) * x) % 289;
    }

    #region Noise functions

    public static float Noise(float x)
    {
        var X = (int)x & 0xff;
        x -= (int)x;
        var u = Fade(x);
        return Lerp(u, Grad(perm(X), x), Grad(perm(X+1), x-1)) * 2;
    }

    public static float Noise(float x, float y)
    {
        var X = (int)x & 0xff;
        var Y = (int)y & 0xff;
        x -= (int)x;
        y -= (int)y;
        var u = Fade(x);
        var v = Fade(y);
        var A = (perm(X  ) + Y) & 0xff;
        var B = (perm(X+1) + Y) & 0xff;
        return Lerp(v, Lerp(u, Grad(perm(A  ), x, y  ), Grad(perm(B  ), x-1, y  )),
               Lerp(u,         Grad(perm(A+1), x, y-1), Grad(perm(B+1), x-1, y-1)));
    }

    public static float Noise(float2 coord)
    {
        return Noise(coord.x, coord.y);
    }

    public static float Noise(float x, float y, float z)
    {
        var X = (int)x & 0xff;
        var Y = (int)y & 0xff;
        var Z = (int)z & 0xff;
        x -= (int)x;
        y -= (int)y;
        z -= (int)z;
        var u = Fade(x);
        var v = Fade(y);
        var w = Fade(z);
        var A  = (perm(X  ) + Y) & 0xff;
        var B  = (perm(X+1) + Y) & 0xff;
        var AA = (perm(A  ) + Z) & 0xff;
        var BA = (perm(B  ) + Z) & 0xff;
        var AB = (perm(A+1) + Z) & 0xff;
        var BB = (perm(B+1) + Z) & 0xff;
        return Lerp(w, Lerp(v, Lerp(u, Grad(perm(AA  ), x, y  , z  ), Grad(perm(BA  ), x-1, y  , z  )),
                               Lerp(u, Grad(perm(AB  ), x, y-1, z  ), Grad(perm(BB  ), x-1, y-1, z  ))),
               Lerp(v, Lerp(u, Grad(perm(AA+1), x, y  , z-1), Grad(perm(BA+1), x-1, y  , z-1)),
                       Lerp(u, Grad(perm(AB+1), x, y-1, z-1), Grad(perm(BB+1), x-1, y-1, z-1))));
    }

    public static float Noise(float3 coord)
    {
        return Noise(coord.x, coord.y, coord.z);
    }

    #endregion

    #region fBm functions

    public static float Fbm(float x, int octave)
    {
        var f = 0.0f;
        var w = 0.5f;
        for (var i = 0; i < octave; i++) {
            f += w * Noise(x);
            x *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    public static float Fbm(float2 coord, int octave)
    {
        var f = 0.0f;
        var w = 0.5f;
        for (var i = 0; i < octave; i++) {
            f += w * Noise(coord);
            coord *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    public static float Fbm(float x, float y, int octave)
    {
        return Fbm(new float2(x, y), octave);
    }

    public static float Fbm(float3 coord, int octave)
    {
        var f = 0.0f;
        var w = 0.5f;
        for (var i = 0; i < octave; i++) {
            f += w * Noise(coord);
            coord *= 2.0f;
            w *= 0.5f;
        }
        return f;
    }

    public static float Fbm(float x, float y, float z, int octave)
    {
        return Fbm(new float3(x, y, z), octave);
    }

    #endregion

    #region Private functions

    static float Fade(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    static float Lerp(float t, float a, float b)
    {
        return a + t * (b - a);
    }

    static float Grad(int hash, float x)
    {
        return (hash & 1) == 0 ? x : -x;
    }

    static float Grad(int hash, float x, float y)
    {
        return ((hash & 1) == 0 ? x : -x) + ((hash & 2) == 0 ? y : -y);
    }

    static float Grad(int hash, float x, float y, float z)
    {
        var h = hash & 15;
        var u = h < 8 ? x : y;
        var v = h < 4 ? y : (h == 12 || h == 14 ? x : z);
        return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
    }

    #endregion
}
