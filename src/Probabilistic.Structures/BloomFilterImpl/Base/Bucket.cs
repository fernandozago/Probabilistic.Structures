namespace Probabilistic.Structures.BloomFilterImpl.Base;

public class Bucket
{
    private bool _isSet = false;

    internal bool IsSet => _isSet;

    internal void Set() =>
        _isSet = true;

    internal void Reset() =>
        _isSet = false;
}
