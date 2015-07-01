using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taken.Generators.Utility
{
    /// <summary>
    /// A class that represents a range of possibility.</summary>
    public class Range
    {
        private double minimum;
        private double maximum;

        /// <summary>
        /// Class constructor.</summary>
        /// <param name="min">The lower bound of the range.</param>
        /// <param name="max">The upper bound of the range.</param>
        public Range(double min, double max)
        {
                this.minimum = min;
                this.maximum = max;
        }

        /// <summary>
        /// Gets the lower bound of the range.</summary>
        /// <returns>The lower bound of the range.</returns>
        public double Min(){
            return this.minimum;
        }


        /// <summary>
        /// Gets the upper bound of the range.</summary>
        /// <returns>The upper bound of the range.</returns>
        public double Max()
        {
            return this.maximum;
        }

        /// <summary>
        /// Determines if a number is in this range.</summary>
        /// <param name="number">The number being evaluated.</param>
        /// <returns>True or false.</returns>
        public bool IsInRange(double number)
        {
            if ((number >= minimum) && (number <= maximum))
                return true;
            return false;
        }
    }
}
