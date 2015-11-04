namespace D
{
    using System;
    using System.Collections.Generic;

    public class PowerComparer : IComparer<Power>
    {
        /// <summary>
        /// Compares two <see cref="Power"/>s without evaluating them.
        /// </summary>
        /// <remarks>
        /// Based on the assumption that a^b &lt; a'^b' \iff log_a a^b &lt; log_a a'^b' \iff b &lt; b' log_a a'.
        /// Since we enter floating arithmetic of doubles some accuracy issues may apply. 
        /// The algorithm can be further improved by determining \epsilon small differences and fall back to
        /// evaluation based on big decimals custom implementation. 
        /// </remarks>
        public int Compare(Power x, Power y)
        {
            if (x.Exponent < y.Exponent * Math.Log(y.Base, x.Base))
            {
                return -1;
            }

            return 1;
        }
    }
}