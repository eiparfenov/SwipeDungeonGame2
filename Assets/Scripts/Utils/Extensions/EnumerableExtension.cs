using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Utils.Extensions
{
    public static class EnumerableExtension
    {
        public static T RandomOrDefaultWithWeight<T>(this IEnumerable<T> source, Func<T, float> weight)
        {
            var sourceArr = source.ToArray();
            if (sourceArr.Length == 0) return default;
            
            var totalWeight = sourceArr.Select(weight).Sum();
            var selectedWeight = Random.value * totalWeight;
            var selectedIdx = 0;
            
            while (selectedWeight - weight(sourceArr[selectedIdx]) > 0)
            {
                selectedWeight -= weight(sourceArr[selectedIdx]);
                selectedIdx++;
            }

            return sourceArr[selectedIdx];
        }

        public static void Foreach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var t in source)
            {
                action(t);
            }
        }

        public static TItem ItemWithMax<TItem, TValue>(this IEnumerable<TItem> source, Func<TItem, TValue> comparer) where TValue: IComparable
        {
            if (source == null) return default;
            var arr = source.ToArray();
            
            if (arr.Length == 0) return default;
            var max = arr[0];
            foreach (var item in arr)
            {
                if (comparer(max).CompareTo(comparer(item)) < 0)
                {
                    max = item;
                }
            }

            return max;
        }
        public static TItem ItemWithMin<TItem, TValue>(this IEnumerable<TItem> source, Func<TItem, TValue> comparer) where TValue: IComparable
        {
            if (source == null) return default;
            var arr = source.ToArray();
            
            if (arr.Length == 0) return default;
            var min = arr[0];
            foreach (var item in arr)
            {
                if (comparer(min).CompareTo(comparer(item)) > 0)
                {
                    min = item;
                }
            }

            return min;
        }
    }
}