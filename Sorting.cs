using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAnArray
{
    static class Sorting
    {
        public static void MergeSort<T>(this T[] input)
        {
            MergeSort<T>(input, Comparer<T>.Default);
        }

        public static void MergeSort<T>(this T[] input, IComparer<T> Comparer)
        {
            MergeSort<T>(input, Comparer.Compare);
        }

        public static void MergeSort<T>(this T[] input, Comparison<T> Comparison)
        {
            if (input.Length <= 1) return;

            T[] left = new T[(input.Length + 1) / 2];
            T[] right = new T[input.Length - left.Length];

            Array.Copy(input, left, left.Length);

            for (int i = left.Length, j = 0; i < input.Length; i++, j++)
            {
                right[j] = input[i];
            }
            
            left.MergeSort(Comparison);
            right.MergeSort(Comparison);

            T[] temp = Merge(left, right, Comparison);
            temp.CopyTo(input, 0);
        }

        private static T[] Merge<T>(T[] Left, T[] Right, Comparison<T> Comparison)
        {
            T[] ret = new T[Left.Length + Right.Length];
            var index = 0;
            var leftCount = Left.Count();
            var rightCount = Right.Count();
            int i = 0, j = 0;

            while (i < leftCount && j < rightCount)
            {
                if (Comparison(Left[i], Right[j]) <= 0)
                {
                    ret[index] = Left[i];
                    i++;
                }
                else
                {
                    ret[index] = Right[j];
                    j++;
                }
                index++;
            }
            while (i < Left.Length)
            {
                ret[index] = Left[i];
                i++;
                index++;
            }

            while (j < Right.Length)
            {
                ret[index] = Right[j];
                j++;
                index++;
            }
            return ret;
        }
    }
}
