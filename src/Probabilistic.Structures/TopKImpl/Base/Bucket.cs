using System.Runtime.CompilerServices;

namespace Probabilistic.Structures.TopKImpl.Base;

internal struct Bucket
{
    private long _counter;
    private uint _fingerprint;
    private readonly Random _random;

    public Bucket()
    {
#if NET5_0
        _random = new Random(Guid.NewGuid().GetHashCode());
#else
        _random = Random.Shared;
#endif
    }

    internal long Set(uint fingerprint, double decay)
    {
        if (_fingerprint == fingerprint)
        {
            return ++_counter;
        }
        else if (Decay(decay))
        {
            _fingerprint = fingerprint; //Seize
            return ++_counter;
        }

        return 0;
    }

    internal readonly long Count(uint fingerprint)
    {
        if (_fingerprint != fingerprint)
            return 0;

        return _counter;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool Decay(double decay)
    {
        if (_counter > 0)
        {
            double probability = Math.Pow(decay, -_counter);
            if (probability >= 1 || probability >= _random.NextDouble())
            {
                _counter--;
            }
        }
        return _counter == 0;
    }
}
