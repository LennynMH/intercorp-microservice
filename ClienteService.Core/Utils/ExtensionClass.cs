using System;
using System.Collections.Generic;
using System.Linq;

namespace ClienteService.Core.Utils
{
    public static class ExtensionClass
    {
        public static double StandardDeviation(this IEnumerable<int> sequence)
        {
            double average = sequence.Average();
            double sum = sequence.Sum(d => Math.Pow(d - average, 2));
            return Math.Sqrt((sum) / sequence.Count());
        }
    }
}
