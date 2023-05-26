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
    }
}