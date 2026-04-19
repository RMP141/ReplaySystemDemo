using UnityEngine;

public class DeterministicRNG : MonoBehaviour, IRng
{
    public void InitState(int seed)
    {
        Random.InitState(seed);
    }

    public int Next(int minInclusive, int maxExclusive)
    {
        return Random.Range(minInclusive, maxExclusive);
    }

    public float NextFloat()
    {
        return Random.value;
    }
}