/*
 * Project: Shape (Unity Native Plugin)
 * Feature: Deterministic Spawn System
 * 
 * [Optimization Note]
 * - Designed to minimize GC allocation in Unity (Managed Heap).
 * - Uses raw pointer arithmetic to process batch data directly.
 * - Ensures deterministic results for replay systems.
 */

// NativeSpawn.cpp
#include <stdint.h>
#include <math.h>

#if defined(_MSC_VER)
#define DLL_EXPORT extern "C" __declspec(dllexport)
#else
#define DLL_EXPORT extern "C"
#endif

struct Vec2 { float x; float y; };

static inline uint32_t xorshift32(uint32_t& state)
{
    uint32_t x = state;
    x ^= x << 13;
    x ^= x >> 17;
    x ^= x << 5;
    state = x;
    return x;
}

static inline float rand01(uint32_t& state)
{
    return (float)xorshift32(state) * (1.0f / 4294967296.0f);
}


static inline float randRange(uint32_t& state, float minv, float maxv)
{
    return minv + (maxv - minv) * rand01(state);
}

static inline uint32_t splitmix32(uint32_t x) {
    x += 0x9e3779b9u;
    x = (x ^ (x >> 16)) * 0x85ebca6bu;
    x = (x ^ (x >> 13)) * 0xc2b2ae35u;
    return x ^ (x >> 16);
}

// "원본 TrySpawnEnemy의 for(i<MAX_TRIES) 루프"와 동일하게:
// - 정확히 maxTries 번 시도
// - 매 시도마다 outer에서 1회 뽑음
// - inner면 그 시도는 무효(표시만 해둠)
// outOffsets[i]는 i번째 시도의 결과(유효면 값, 무효면 x=NaN)
DLL_EXPORT void GenerateSpawnOffsets_ExactTries(Vec2 innerHalf, Vec2 outerHalf, int maxTries, uint32_t* rngState, Vec2* outOffsets)
{
    if (!outOffsets || !rngState || maxTries <= 0) return;

    uint32_t& rng = *rngState;
    if (rng == 0) rng = 2463534242u; // safety

    for (int i = 0; i < maxTries; ++i)
    {
        float ox = randRange(rng, -outerHalf.x, outerHalf.x);
        float oy = randRange(rng, -outerHalf.y, outerHalf.y);

        if (fabsf(ox) < innerHalf.x && fabsf(oy) < innerHalf.y)
        {
            outOffsets[i] = Vec2{ NAN, 0.0f };
            continue;
        }

        outOffsets[i] = Vec2{ ox, oy };
    }
}

