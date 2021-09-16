using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using System;

public static class ArrayExtensions
{
    public static void Foreach<T>(this T[,] arr, UnityAction<T> action)
    {
        for (int r = 0; r < arr.GetLength(0); r++)
        {
            for (int c = 0; c < arr.GetLength(1); c++)
            {
                action(arr[r, c]);
            }
        }
    }
}

public static class IEnumerableExtensions
{
    public static T MinItem<T>(this IEnumerable<T> items, Func<T, decimal> selector)
    {
        decimal minValue = decimal.MaxValue;
        T minItem = default;
        foreach (T item in items)
        {
            decimal itemValue = selector(item);
            if (minItem.Equals(default(T)) || itemValue < minValue)
            {
                minValue = itemValue;
                minItem = item;
            }
        }
        return minItem;
    }

    public static T MinItem<T>(this IEnumerable<T> items, Func<T, double> selector)
    {
        double minValue = double.MaxValue;
        T minItem = default;
        foreach (T item in items)
        {
            double itemValue = selector(item);
            if (minItem.Equals(default(T)) || itemValue < minValue)
            {
                minValue = itemValue;
                minItem = item;
            }
        }
        return minItem;
    }
}
