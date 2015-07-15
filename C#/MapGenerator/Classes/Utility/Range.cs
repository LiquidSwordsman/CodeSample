using System.Collections.Generic;

namespace CodeSample.Utility
{
    /// <summary>
    /// A class that represents a range of possibility.</summary>
    public struct Range
    {
        public float min;
        public float max;

        /// <summary>
        /// Class constructor.</summary>
        /// <param name="min">The lower bound of the range.</param>
        /// <param name="max">The upper bound of the range.</param>
        public Range(float num1, float num2)
        {
            List<float> items = new List<float>(){num1, num2};
            items.Sort();
            this.min = items[0];
            this.max = items[1];
        }

        public Range(List<int> input)
        {
            input.Sort();
            this.min = input[0];
            this.max = input[1];
        }

        /// <summary>
        /// Determines if a number is in this range.</summary>
        /// <param name="number">The number being evaluated.</param>
        /// <returns>True or false.</returns>
        public bool IsInRange(double number)
        {
            if ((number >= min) && (number <= max))
                return true;
            return false;
        }

        public static bool IsInRange(double number, double rangeMin, double rangeMax)
        {
            if ((number >= rangeMin) && (number <= rangeMax))
                return true;
            return false;
        }

        public override string ToString()
        {
            return "[" + min.ToString() + ", " + max.ToString() + "]";
        }
    }
}
