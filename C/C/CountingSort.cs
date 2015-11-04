using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace C
{
    using System.CodeDom;
    using System.Collections;
    using System.Runtime;
    using System.Runtime.CompilerServices;
    using System.Security.Principal;

    /// <summary>
    /// Counting sort.
    /// </summary>
    /// <remarks>
    /// Uses linear number of steps with pessimistic time of \omega(n+k), where n is number of items in 
    /// set and k is range of possible values. 
    /// Class it *not* thread safe. 
    /// For more details on the algorithm please check https://en.wikipedia.org/wiki/Counting_sort.
    /// </remarks>
    public class CountingSort
    {
        private readonly byte[] count;

        public CountingSort(byte maxValue)
        {
            this.count = new byte[maxValue + 1];
            Array.Clear(this.count, 0, this.count.Length);
        }

        public byte[] Sort(byte[] input)
        {
            if (input.Length > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(input),
                    $"You can sort arrays of up to {byte.MaxValue} elements only. ");
            }

            foreach (var value in input)
            {
                this.count[value]++;
            }

            byte total = 0;
            for (int i = 0; i < this.count.Length; i++)
            {
                var oldCount = this.count[i];
                this.count[i] = total;
                total += oldCount;
            }

            var output = new byte[input.Length];
            foreach (var value in input)
            {
                output[this.count[value]] = value;
                this.count[value]++;
            }

            return output;
        }
    }
}
