using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D
{
    public class PowersSorter
    {
        /// <summary>
        /// Sorts given array of <see cref="Power"/>s. 
        /// </summary>
        /// <param name="input">Array of <see cref="Power"/>s to be sorted. </param>
        public void Sort(Power[] input)
        {
            Array.Sort(input, new PowerComparer());
        }
    }
}
