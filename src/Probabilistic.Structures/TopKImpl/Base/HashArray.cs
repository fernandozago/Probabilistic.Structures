namespace Probabilistic.Structures.TopKImpl.Base
{
    internal readonly struct HashArray<T>
    {
        private readonly Bucket[] _buckets;
        private readonly uint _seed;
        private readonly double _decay;

#if NET5_0
        private readonly Random _random = new (Guid.NewGuid().GetHashCode());
#endif

        internal HashArray(int width, double decay) =>
#if NET5_0
            (_decay, _buckets, _seed) = (decay, new Bucket[width], (uint)Guid.NewGuid().GetHashCode() + (uint)_random.Next(0, width));
#else
            (_decay, _buckets, _seed) = (decay, new Bucket[width], (uint)Guid.NewGuid().GetHashCode() + (uint)Random.Shared.Next(0, width));
#endif

        internal readonly long CountBucket(T data) =>
            GetBucketAndFingerprint(data, out uint fingerprint)
                .Count(fingerprint);

        internal readonly long SetBucket(T data) =>
            GetBucketAndFingerprint(data, out uint fingerprint)
                .Set(fingerprint, _decay);

        private readonly ref Bucket GetBucketAndFingerprint(T data, out uint fingerprint) =>
            ref _buckets[(int)((fingerprint = GetFingerprint(data)) % _buckets.Length)];

        private readonly uint GetFingerprint(T data) =>
            (uint)data!.GetHashCode() + _seed;

    }
}
