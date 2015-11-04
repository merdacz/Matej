using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace C
{
    using System.Runtime;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Sorting network for 11 elements using best known arrangement. 
    /// </summary>
    /// <remarks>
    /// Uses 36 swaps. No additional memory is being used except temporary slot for swapping. 
    /// Based on best known Swapping scheme generated from http://pages.ripco.net/~jgamble/nw.html. 
    /// For more details on sorting networks theory check https://en.wikipedia.org/wiki/Sorting_network.
    /// </remarks>
    public class SortingNetwork11
    {
        public void Sort(byte[] input)
        {
            if (input.Length != 11)
            {
                throw new ArgumentOutOfRangeException(nameof(input), "You can sort arrays of 11 elements only. ");
            }

            byte temp = default(byte);
            Swap(input, ref temp, 0, 1);
            Swap(input, ref temp, 2, 3);
            Swap(input, ref temp, 4, 5);
            Swap(input, ref temp, 6, 7);
            Swap(input, ref temp, 8, 9);
            Swap(input, ref temp, 1, 3);
            Swap(input, ref temp, 5, 7);
            Swap(input, ref temp, 0, 2);
            Swap(input, ref temp, 4, 6);
            Swap(input, ref temp, 8, 10);
            Swap(input, ref temp, 1, 2);
            Swap(input, ref temp, 5, 6);
            Swap(input, ref temp, 9, 10);
            Swap(input, ref temp, 1, 5);
            Swap(input, ref temp, 6, 10);
            Swap(input, ref temp, 5, 9);
            Swap(input, ref temp, 2, 6);
            Swap(input, ref temp, 1, 5);
            Swap(input, ref temp, 6, 10);
            Swap(input, ref temp, 0, 4);
            Swap(input, ref temp, 3, 7);
            Swap(input, ref temp, 4, 8);
            Swap(input, ref temp, 0, 4);
            Swap(input, ref temp, 1, 4);
            Swap(input, ref temp, 7, 10);
            Swap(input, ref temp, 3, 8);
            Swap(input, ref temp, 2, 3);
            Swap(input, ref temp, 8, 9);
            Swap(input, ref temp, 2, 4);
            Swap(input, ref temp, 7, 9);
            Swap(input, ref temp, 3, 5);
            Swap(input, ref temp, 6, 8);
            Swap(input, ref temp, 3, 4);
            Swap(input, ref temp, 5, 6);
            Swap(input, ref temp, 7, 8);

        }

        /// <summary>
        /// Swaps given elements tab[i] and tab[j] values to assure tab[i] &lt;= tab[j]
        /// </summary>
        /// <remarks>
        /// Uses shared temp variable to keep memory usage as limited as possible still
        /// assuring thread safety. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Swap(byte[] tab, ref byte temp, int i, int j)
        {
            if (tab[i] > tab[j])
            {
                temp = tab[i];
                tab[i] = tab[j];
                tab[j] = temp;
            }
        }
    }
}
