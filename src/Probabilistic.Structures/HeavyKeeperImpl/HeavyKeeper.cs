using Probabilistic.Structures.HeavyKeeperImpl.Base;

namespace Probabilistic.Structures.HeavyKeeperImpl;

#nullable disable
public sealed class HeavyKeeper<T>
    where T : IEquatable<T>
{

    private readonly object _syncRoot = new();
    private int _k;
    private int _width;
    private int _depth;
    private double _decay;
    private HashArray<T>[] _hashArrays;
    private MaxHeap<T> _maxHeap;

    /// <summary>
    /// Initialization of a TopK Probalistic Algorithm
    /// </summary>
    /// <param name="k">Size of the Top items that must be tracked</param>
    /// <param name="depth">How many arrays we want to create a fingerprint of items</param>
    /// <param name="width">How many buckets we have inside each array</param>
    /// <param name="decay">How often the items will be decaying</param>
    public HeavyKeeper(int k, int depth, int width, double decay)
    {
        Reset(k, depth, width, decay);
    }

    public void Add(T data)
    {
        long count = 0;

        lock (_syncRoot)
        {
            for (var i = 0; i < _hashArrays!.Length; i++)
            {
                count = Math.Max(count, _hashArrays[i].SetBucket(data));
            }

            _maxHeap.Add(data, count);
        }
    }

    public void Reset(int k, int depth, int width, double decay)
    {
        lock (_syncRoot)
        {
            _k = k;
            _width = width;
            _depth = depth;
            _decay = decay;
        }
        Reset();
    }

    public void Reset()
    {
        lock (_syncRoot)
        {
            _maxHeap = new(_k);
            _hashArrays = Enumerable.Range(0, _depth).Select(_ => new HashArray<T>(_width, _decay)).ToArray();
        }
    }

    public long Count(T data)
    {
        if (_maxHeap.CountIfAny(data, out long count))
        {
            return count;
        }

        for (int i = 0; i < _hashArrays!.Length; i++)
        {
            count = Math.Max(count, _hashArrays[i].CountBucket(data));
        }
        return count;
    }

    public bool Any(T data) =>
        _maxHeap.IndexOf(data) > -1;

    public Item<T>[] Top() =>
        _maxHeap.Top();

}
