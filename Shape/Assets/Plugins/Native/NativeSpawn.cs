using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class NativeSpawn
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec2
    {
        public float x;
        public float y;
        public Vec2(float x, float y) { this.x = x; this.y = y; }
    }

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private const string DLL_NAME = "NativeSpawn";
#else
    private const string DLL_NAME = "__Internal";
#endif

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void GenerateSpawnOffsets_ExactTries(
        Vec2 innerHalf,
        Vec2 outerHalf,
        int maxTries,
        ref uint seed,
        [Out] Vec2[] outOffsets
    );

    public static void GenerateOffsetsExactTries(Vector2 innerHalf, Vector2 outerHalf, int maxTries, ref uint seed, Vec2[] buffer)
    {
        if (buffer == null || buffer.Length < maxTries)
            throw new ArgumentException($"buffer size must be >= {maxTries}");

        GenerateSpawnOffsets_ExactTries(
            new Vec2(innerHalf.x, innerHalf.y),
            new Vec2(outerHalf.x, outerHalf.y),
            maxTries,
            ref seed,
            buffer
        );
    }
}

