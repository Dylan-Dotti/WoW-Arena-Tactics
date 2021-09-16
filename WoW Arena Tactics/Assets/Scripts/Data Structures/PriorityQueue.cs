using System;
using System.Collections;
using System.Collections.Generic;


public class PriorityQueue<T> : IEnumerable<T>
{
    private readonly List<T> pool = new List<T>();
    private readonly Func<T, T, int> valueComparator;
    private readonly PriorityOptions priorityOption;

    public int Count => pool.Count;
    public bool IsEmpty => Count == 0;

    public PriorityQueue(Func<T, T, int> valueComparator,
        PriorityOptions priorityOption = PriorityOptions.MinFirst)
    {
        this.valueComparator = valueComparator;
        this.priorityOption = priorityOption;
    }

    public void Enqueue(T item)
    {
        pool.Add(item);
    }

    public T PriorityDequeue()
    {
        if (pool.Count == 0) return default;
        int bestIndex = -1;
        T bestItem = default;
        for (int i = 0; i < pool.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(bestItem, default) || 
                (priorityOption == PriorityOptions.MinFirst && valueComparator(bestItem, pool[i]) > 0) ||
                (priorityOption == PriorityOptions.MaxFirst && valueComparator(bestItem, pool[i]) < 0))
            {
                bestIndex = i;
                bestItem = pool[i];
            }
        }
        pool.RemoveAt(bestIndex);
        return bestItem;
    }

    public void Clear()
    {
        pool.Clear();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return pool.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return pool.GetEnumerator();
    }
}

public enum PriorityOptions
{
    MinFirst, MaxFirst
}
