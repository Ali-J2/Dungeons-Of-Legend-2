using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DungeonBrickStudios
{
    public static class ArrayExtension
    {
        public static T GetRandomElement<T>(this T[] array)
        {
            int index = Random.Range(0, array.Length);
            return array[index];
        }

        public static int FilterForType<T>(this Collider[] array, int count, T[] targetArray) where T : Component
        {
            int newCount = 0;
            for(int i = 0; i < count; i++)
            {
                Collider collider = array[i];
                T matchingType;
                if (!collider.TryGetComponent(out matchingType))
                    continue;

                if (targetArray.Contains(matchingType, newCount))
                    continue;

                targetArray[newCount++] = matchingType;
            }

            return newCount;
        }

        public static bool Contains<T>(this T[] array, T element, int countIndex)
        {
            for (int i = 0; i < countIndex; i++)
                if (array[i].Equals(element))
                    return true;

            return false;
        }
    }
}
