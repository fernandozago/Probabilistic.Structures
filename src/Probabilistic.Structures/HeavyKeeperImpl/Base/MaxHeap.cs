namespace Probabilistic.Structures.HeavyKeeperImpl.Base;

internal class MaxHeap<T>
    where T : IEquatable<T>
{
    private readonly int _k;
    private readonly List<Item<T>> _heap;

    internal MaxHeap(int k)
    {
        _k = k;
        _heap = new List<Item<T>>(k);
    }

    internal void Add(T data, long count)
    {
        if ((count <= 0 || _heap.Count >= _k) && count < _heap[^1].Count) //dont bother trying to updating or adding when the count is less than the lower values on this MaxHeap
        {
            return;
        }

        if (!TryUpdateExisting(data))
        {
            if (_heap.Count < _k) //if there is space on the Item<T>[], we add it to the list
            {
                _heap.Add(new(data, count));
            }
            else if (count >= _heap[^1].Count)
            {
                _heap[^1].Set(data, count);
                Rollup();
            }
        }
    }

    internal Item<T>[] Top() =>
        _heap.ToArray();

    internal int IndexOf(T data)
    {
        for (int i = 0; i < _heap.Count; i++)
        {
            if (_heap[i].Is(data))
            {
                return i;
            }
        }

        return -1;
    }

    internal bool CountIfAny(T data, out long count)
    {
        if (IndexOf(data) is int i && i > -1)
        {
            count = _heap[i].Count;
            return true;
        }

        count = 0;
        return false;
    }

    private bool TryUpdateExisting(T data)
    {
        for (int i = 0; i < _heap.Count; i++)
        {
            if (_heap[i].Is(data))
            {
                _heap[i].Increment();
                Rollup(i);
                return true;
            }
        }
        return false;
    }

    private void Rollup() =>
        Rollup(_heap.Count - 1);

    private void Rollup(int index)
    {
        int previousIndex = index - 1;
        while (previousIndex >= 0 && _heap[previousIndex].Count < _heap[index].Count)
        {
            (_heap[index], _heap[previousIndex]) = (_heap[previousIndex--], _heap[index--]);
        }
    }
}
