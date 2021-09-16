using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPriorityQueue : MonoBehaviour
{
    private void Start()
    {
        int comparator(int x, int y) => x.CompareTo(y);
        var queue = new PriorityQueue<int>(comparator, PriorityOptions.MaxFirst);
        var inputItems = new List<int> { 3, 1, 2, 5, 4 };
        foreach (var item in inputItems)
        {
            queue.Enqueue(item);
        }
        while (!queue.IsEmpty)
        {
            Debug.Log(queue.PriorityDequeue());
        }
    }
}
