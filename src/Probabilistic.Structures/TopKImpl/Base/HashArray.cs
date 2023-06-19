namespace Probabilistic.Structures.TopKImpl.Base;

internal class HashArray<T>
{
    private readonly Bucket[] _buckets;
    private readonly uint _seed;
    private readonly double _decay;
    private readonly Random _random =
#if NET5_0
        new (Guid.NewGuid().GetHashCode());
#else
        Random.Shared;
#endif

    internal HashArray(int width, double decay) =>
        (_decay, _buckets, _seed) = (decay, Enumerable.Range(0, width).Select(_ => new Bucket()).ToArray(), (uint)Guid.NewGuid().GetHashCode() + (uint)_random.Next(0, width));

    internal long CountBucket(T data) =>
        GetBucketAndFingerprint(data, out uint fingerprint)
            .Count(fingerprint);

    internal long SetBucket(T data) =>
        GetBucketAndFingerprint(data, out uint fingerprint)
            .Set(fingerprint, _decay);

    private Bucket GetBucketAndFingerprint(T data, out uint fingerprint) =>
        _buckets[(int)((fingerprint = GetFingerprint(data)) % _buckets.Length)];

    private uint GetFingerprint(T data) =>
        (uint)data!.GetHashCode() + _seed;

}
