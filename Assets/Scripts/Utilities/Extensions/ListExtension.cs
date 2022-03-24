using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DungeonBrickStudios
{
    public static class ListExtension
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            int index = Random.Range(0, list.Count);
            return list[index];
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
