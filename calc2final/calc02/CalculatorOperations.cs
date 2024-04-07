using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc02
{
    public class CalculatorOperations
    {
        public static string PerformSquareRoot(string input)
        {
            double result = Math.Sqrt(Double.Parse(input));
            return $"√({input})";
        }

        public static string PerformSquare(string input)
        {
            double value = Convert.ToDouble(input);
            double result = value * value;
            return $"({input})²";
        }

        public static string PerformReciprocal(string input)
        {
            double result = 1.0 / Convert.ToDouble(input);
            return $"1/({input})";
        }

        public static string PerformPercentage(string input)
        {
            double result = Convert.ToDouble(input) / 100;
            return $"({input})%";
        }

        public static string PerformNegation(string input)
        {
            double result = -1 * Convert.ToDouble(input);
            return $"(Negace {input})";
        }
    }
}
