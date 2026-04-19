public interface IRng
{
    void InitState(int seed);
    int Next(int minInclusive, int maxExclusive);
    float NextFloat();
}