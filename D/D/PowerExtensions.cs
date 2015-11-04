namespace D
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PowerExtensions
    {
        /// <summary>
        /// Evaluates power list creating list of computer values. 
        /// </summary>
        /// <remarks>
        /// Use with care, will work correctly only for relatively small values. 
        /// </remarks>
        public static IEnumerable<double> ComputePowers(this Power[] input)
        {
            return input.Select(x => Math.Pow(x.Base, x.Exponent));
        }
    }
}